using OPCAutomation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KepCom
{
    public partial class Frm_OpcClient : Form
    {
        public Frm_OpcClient()
        {
            InitializeComponent();
            //
            CheckForIllegalCrossThreadCalls = false;
            this.Load += Frm_OpcClient_Load;
        }

        #region 变量区
        //用于ListBox中的数据显示
        private ListViewItem listitem;
        private Dictionary<string, OPCGroup> groupMap = new Dictionary<string, OPCGroup>();
        private Dictionary<int, OPCItem> itemMap = new Dictionary<int, OPCItem>();

        //定义OPC服务器
        private OPCServer kepServer;
        //定义OPC服务器组集
        OPCGroups kepGruops;
        //定义OPC服务器组
        OPCGroup kepGruop;
        //定义OPC服务器项目集
        OPCItems kepItems;
        //定义OPC服务器浏览器
        private OPCBrowser kepBrowser;
        //定义OPC变量集合
        List<OpcHelperItem> OPCList = new List<OpcHelperItem>();
        List<int> serverHandles = new List<int>();
        List<int> ClientHandles = new List<int>();
        List<string> TempIDList = new List<string>();
        //定义返回的OPC标签错误
        Array iErrors;
        //定义要添加的OPC标签的标识符
        Array strTempIDs;
        Array strClientHandles;
        Array strServerHandles;
        Array readServerHandles;
        Array writeServerHandles;
        private Array writeArrayHandles;
        Array readError;
        Array writeError;
        int readTransID;
        int writeTransID;
        int readCancelID;
        int writeCancelID;
        #endregion

        private void Frm_OpcClient_Load(object sender, EventArgs e)
        {
            OPC opc = new OPC();
            this.btn_RefreshList_Click(null, null);
            this.timer1.Interval = 100;
            this.timer1.Enabled = true;
            this.timer1.Tick += Timer1_Tick;
            this.dgv_data.AutoGenerateColumns = false;
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            this.lbl_CurrentTime.Text = "当前时间: " + DateTime.Now.ToLongTimeString();
            if (kepServer != null)
            {
                if (kepServer.ServerState == (int)OPCServerState.OPCRunning)
                {
                    this.lbl_Status.Text = Enum.GetName(typeof(StatusHelper.ConnectHelper), StatusHelper.ConnectHelper.Connect);
                }
                else
                {
                    this.lbl_Status.Text = Enum.GetName(typeof(StatusHelper.ConnectHelper), StatusHelper.ConnectHelper.DisConnect);
                }
            }
            if (this.OPCList.Count > 0)
            {
                if (kepServer != null)
                {
                    kepGruop.AsyncRead(this.OPCList.Count, ref readServerHandles, out readError, readTransID, out readCancelID);
                }
            }
        }

        private static object obj = new object();
        private void btn_RefreshList_Click(object sender, EventArgs e)
        {
            //启动任务 开一个新的线程去运行,不然导致速度太慢
            Task.Factory.StartNew(() =>
            {
                this.cmb_ServerNode.Items.Clear();
                IPHostEntry iphost = Dns.GetHostEntry(Environment.MachineName);
                if (iphost.AddressList.Length > 0)
                {
                    foreach (var item in iphost.AddressList)
                    {
                        string hostName = Dns.GetHostEntry(item.ToString()).HostName;
                        //上锁,用来防止多加hostName
                        lock (obj)
                        {
                            if (!this.cmb_ServerNode.Items.Contains(hostName))
                            {
                                this.cmb_ServerNode.Items.Add(hostName);
                            }
                        }

                    }
                }
                else
                {
                    return;
                }
            });

        }

        //用来标识每一个OPCItem数据的唯一标识
        private void SetItemsClientHandle()
        {
            foreach (var item in this.itemMap.Values)
            {
                item.ClientHandle = item.GetHashCode();
            }
        }

        //此方法通过OPCBrowser将数据结节加载到TreeView中.
        private bool LoadDataToTree(TreeNode node, string nodeName)
        {
            TreeNode childNode = null;
            OPCGroup group = null;
            try
            {
                this.kepBrowser.ShowBranches();
                int count = this.kepBrowser.Count;
                if (count == 0)
                {
                    this.kepBrowser.ShowLeafs(true);
                    group = this.kepServer.OPCGroups.Add(nodeName);
                    group.UpdateRate = 1000;
                    group.IsSubscribed = true;
                    group.IsActive = true;
                    node.Tag = group;
                    this.groupMap.Add(nodeName, group);
                }
                HashSet<string> itemList = new HashSet<string>();
                foreach (object turn in this.kepBrowser)
                {
                    string path = nodeName;
                    string name = turn.ToString();

                    if (count != 0)
                    {
                        childNode = node.Nodes.Add(name);
                        if (path != "")
                        {
                            path += ".";
                        }
                        path += name;

                        this.kepBrowser.MoveDown(name);
                        LoadDataToTree(childNode, path);
                        this.kepBrowser.MoveUp();
                    }
                    else
                    {
                        OPCItem opcItem = group.OPCItems.AddItem(name, 0);
                        itemList.Add(name);
                        if (!this.itemMap.ContainsKey(opcItem.GetHashCode()))
                        {
                            this.itemMap.Add(opcItem.GetHashCode(), opcItem);
                        }
                    }
                }

                this.kepBrowser.ShowLeafs(false);
                List<string> leafItems = new List<string>();
                foreach (object obj in this.kepBrowser)
                {
                    string itemId = this.kepBrowser.GetItemID(obj.ToString());
                    if (itemList.Contains(itemId))
                    {
                        continue;
                    }
                    leafItems.Add(itemId);
                }

                if (leafItems.Count != 0)
                {
                    group = this.kepServer.OPCGroups.Add(nodeName);
                    group.UpdateRate = 1000;
                    group.IsSubscribed = true;
                    group.IsActive = true;
                    this.groupMap.Add(nodeName, group);
                    node.Tag = group;
                    foreach (string t in leafItems)
                    {
                        OPCItem opcItem = group.OPCItems.AddItem(t, 0);
                        //childNode = node.Nodes.Add(t.ToString());
                        if (!this.itemMap.ContainsKey(opcItem.GetHashCode()))
                        {
                            this.itemMap.Add(opcItem.GetHashCode(), opcItem);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            return true;
        }

        //此方法将选中的叶子节点(OPCGroup)下的所有OPCItem显示在ListView中.
        private void tvwGroupList_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                TreeNode crtNode = this.tvwGroupList.GetNodeAt(e.X, e.Y);
                TreeNode oldNode = this.tvwGroupList.SelectedNode;
                if (oldNode != null)
                {
                    this.List_Items.Items.Clear();
                    if (oldNode.Tag is OPCGroup)
                    {
                        OPCGroup old_group = oldNode.Tag as OPCGroup;
                    }
                }
                this.tvwGroupList.SelectedNode = crtNode;
                if (crtNode.Tag is OPCGroup)
                {
                    OPCGroup crt_group = crtNode.Tag as OPCGroup;
                    OPCItems items = crt_group.OPCItems;
                    if (items.Count != 0)
                    {
                        foreach (OPCItem item in items)
                        {
                            string itemId = item.ItemID;
                            string itemValue = Convert.ToString(item.Value);

                            if (item.CanonicalDataType == 11)
                            {
                                itemValue = Convert.ToInt32(Convert.ToBoolean(itemValue)).ToString();
                            }
                            listitem = new ListViewItem(new string[] {
                    itemId,
                    //item.CanonicalDataType.ToString(),
                    itemValue,
                    item.TimeStamp.ToLocalTime().ToString(),
                    item.Quality.ToString(),
                    //"0"
                            });
                            listitem.Tag = item.ClientHandle;
                            //获取文本里的值,而不是整个字符串
                            this.List_Items.Items.Add(listitem.Text);
                        }

                    }
                    else
                    {
                        Array branches = crt_group.Name.Split('.');
                        this.kepBrowser.MoveTo(ref branches);
                        this.kepBrowser.ShowLeafs(true);
                        foreach (object turn in this.kepBrowser)
                        {
                            string name = turn.ToString();
                            //crt_group.OPCItems.AddItem(name, this.itemHandleClient);
                            ListViewItem listitem = new ListViewItem(new string[] {
                            name,
                            "",
                            "",
                            "",
                            "",
                            ""});
                            this.List_Items.Items.Add(listitem.Text);
                            //this.lstviewItems.Items.Add(listitem);
                        }
                        this.kepBrowser.ShowLeafs(true);
                        this.kepBrowser.MoveToRoot();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }

        /// <summary>
        /// 确定本地主机时,动态改变连接的kepServer主机名
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmb_ServerNode_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.cmb_ServerName.Items.Clear();
            kepServer = new OPCServer();
            object serverList = kepServer.GetOPCServers(this.cmb_ServerNode.Text.Trim());
            foreach (var item in (Array)serverList)
            {
                this.cmb_ServerName.Items.Add(item);
            }
        }

        /// <summary>
        /// 连接kepServer 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Connect_Click(object sender, EventArgs e)
        {
            //获取连接状态
            if (this.btn_Connect.Text == Enum.GetName(typeof(StatusHelper.ConnectHelper), StatusHelper.ConnectHelper.Connect))
            {
                try
                {
                    if (kepServer == null)
                    {
                        kepServer = new OPCServer();
                        kepServer.Connect(this.cmb_ServerName.Text.Trim(), this.cmb_ServerNode.Text.Trim());
                    }
                    else
                    {
                        kepServer.Connect(this.cmb_ServerName.Text.Trim(), this.cmb_ServerNode.Text.Trim());
                    }
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message + "连接失败!");
                }

                this.btn_Connect.Text = Enum.GetName(typeof(StatusHelper.ConnectHelper),
                    StatusHelper.ConnectHelper.DisConnect);
                //kepGroups对象赋值
                kepGruops = kepServer.OPCGroups;
                //kepGroups属性设置
                kepGruops.DefaultGroupDeadband = 0;
                kepGruops.DefaultGroupIsActive = true;

                kepGruop = kepGruops.Add("group1");
                kepGruop.IsActive = true;
                kepGruop.IsSubscribed = true;
                kepGruop.UpdateRate = 100;
                kepGruop.AsyncReadComplete += KepGruop_AsyncReadComplete;

                this.tvwGroupList.Nodes.Clear();
                this.groupMap.Clear();
                this.itemMap.Clear();
                var prog = this.cmb_ServerName.Text.Trim();

                TreeNode node = this.tvwGroupList.Nodes.Add(prog);
                node.Tag = this.kepServer;
                this.kepBrowser = this.kepServer.CreateBrowser();
                LoadDataToTree(node, "");
                this.SetItemsClientHandle();
                //this.LoadConfig(prog);
                //用于显示连接状态,目前无用,先进行删除
                //this.tsslblServerState.Text = "OPC服务[" + prog + "]已连接";
                //this.tsslblItemCount.Text = "Item Count " + this.itemMap.Count.ToString();
                //this.tsslblGroupCount.Text = "Group Count " + this.groupMap.Count.ToString();

            }
            else
            {
                //断开连接,所需要的处理
                if (kepServer != null)
                {
                    kepServer.Disconnect();
                    kepServer = null;
                    this.dgv_data.DataSource = null;
                    this.tvwGroupList.Nodes.Clear();
                    this.groupMap.Clear();
                    this.itemMap.Clear();
                    //清空listbox
                    this.List_Items.Items.Clear();
                    this.btn_Connect.Text = StatusHelper.ConnectHelper.Connect.ToString();
                    this.lbl_Status.Text = StatusHelper.ConnectHelper.DisConnect.ToString();
                }
            }
        }

        private void KepGruop_AsyncReadComplete(int TransactionID, int NumItems, ref Array ClientHandles, ref Array ItemValues, ref Array Qualities, ref Array TimeStamps, ref Array Errors)
        {
            for (int i = 1; i <= NumItems; i++)
            {
                object value = ItemValues.GetValue(i);
                if (value != null)
                {
                    this.OPCList[i - 1].Value = value.ToString();
                    this.OPCList[i - 1].Time = ((DateTime)TimeStamps.GetValue(1)).ToString();
                    if ((int)Qualities.GetValue(1) == 192)
                    {
                        this.OPCList[i - 1].Quality = StatusHelper.ConnectHelper.Good.ToString();
                    }
                    else
                    {
                        this.OPCList[i - 1].Quality = StatusHelper.ConnectHelper.Bad.ToString();
                    }

                }
            }
            this.dgv_data.DataSource = null;
            this.dgv_data.DataSource = this.OPCList;

        }

        private void List_Items_DoubleClick(object sender, EventArgs e)
        {
            DoubleOrClick();
        }
        private void add_Click(object sender, EventArgs e)
        {
            DoubleOrClick();
        }

        //双击或者右键添加的时候执行此功能
        private void DoubleOrClick()
        {
            if (this.List_Items.SelectedItem != null)
            {
                OPCList.Add(new OpcHelperItem()
                {
                    Tag = this.List_Items.SelectedItem.ToString()
                });
            }

            TempIDList.Clear();
            ClientHandles.Clear();
            TempIDList.Add("0");
            ClientHandles.Add(0);
            int count = this.OPCList.Count;
            for (int i = 0; i < count; i++)
            {
                TempIDList.Add(this.OPCList[i].Tag);
                ClientHandles.Add(i + 1);
            }
            strTempIDs = (Array)TempIDList.ToArray();
            strClientHandles = (Array)ClientHandles.ToArray();
            kepItems = kepGruop.OPCItems;
            kepItems.AddItems(this.OPCList.Count, ref strTempIDs, ref strClientHandles, out strServerHandles, out iErrors);
            serverHandles.Clear();
            serverHandles.Add(0);
            for (int i = 0; i < count; i++)
            {
                serverHandles.Add(Convert.ToInt32(strServerHandles.GetValue(i + 1)));
            }
            readServerHandles = (Array)serverHandles.ToArray();
        }

        private void dgv_data_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dgv_data.SelectedRows != null)
            {
                int index = this.dgv_data.CurrentRow.Index;
                OpcHelperItem objItem = this.OPCList[index];
                Frm_Modify objFrm = new Frm_Modify(objItem.Value);
                DialogResult res = objFrm.ShowDialog();
                int[] serverHandle = new int[]
                {
                    0,
                    Convert.ToInt32(strServerHandles.GetValue(index + 1))
                };
                object[] values = new object[2];
                string[] modifyResult;
                writeServerHandles = (Array)serverHandle;
                if (res == DialogResult.OK)
                {
                    modifyResult = objFrm.Tag.ToString().Split('|');
                    values[1] = modifyResult[0];
                    writeArrayHandles = (Array)values;
                    if (modifyResult[1] == "1")
                    {
                        kepGruop.AsyncWrite(1, writeServerHandles, writeArrayHandles, out writeError, writeTransID, out writeCancelID);
                    }
                    else
                    {
                        kepGruop.SyncWrite(1, writeServerHandles, writeArrayHandles, out writeError);
                    }
                }
            }

        }
    }
}
