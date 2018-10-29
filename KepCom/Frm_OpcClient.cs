using OPCAutomation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KepCom
{
    public partial class Frm_OpcClient : Form
    {
        public Frm_OpcClient()
        {
            InitializeComponent();
            this.Load += Frm_OpcClient_Load;
        }

        #region 私有变量
        private OPCBrowser opcBrowser;
        private Dictionary<string, OPCGroup> groupMap = new Dictionary<string, OPCGroup>();
        private Dictionary<int, OPCItem> itemMap = new Dictionary<int, OPCItem>();
        #endregion

        #region 变量区
        //定义OPC服务器
        private OPCServer kepServer;
        //定义OPC服务器组集
        OPCGroups kepGruops;
        //定义OPC服务器组
        OPCGroup kepGruop;
        //定义OPC服务器项目集
        OPCAutomation.OPCItems kepItems;
        //定义OPC服务器浏览器
        OPCBrowser kepBrowser;
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
        Array writeArrayHandles;
        Array readError;
        Array writeError;
        int readTransID;
        int writeTransID;
        int readCancelID;
        int writeCancelID;
        #endregion

        private void Frm_OpcClient_Load(object sender, EventArgs e)
        {
            this.btn_RefreshList_Click(null, null);
            this.timer1.Interval = 1000;
            this.timer1.Enabled = true;
            this.timer1.Tick += Timer1_Tick;
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            this.lbl_CurrentTime.Text = "当前时间: " + DateTime.Now.ToLongTimeString();

            if (kepServer != null)
            {
                if (kepServer.ServerState == (int)OPCServerState.OPCRunning)
                {
                    this.lbl_Status.Text = Enum.GetName(typeof(StatusHelper.ConnectHelper),StatusHelper.ConnectHelper.Connect);
                }
                else
                {
                    this.lbl_Status.Text = Enum.GetName(typeof(StatusHelper.ConnectHelper), StatusHelper.ConnectHelper.DisConnect);
                }
            }

            //if (this.OPCList.Count > 0)
            //{
            //    if (kepServer != null)
            //    {
            //        kepGruop.AsyncRead(this.OPCList.Count, ref readServerHandles, out readError, readTransID, out readCancelID);
            //    }
            //}
        }

        private void btn_RefreshList_Click(object sender, EventArgs e)
        {
            this.cmb_ServerNode.Items.Clear();
            IPHostEntry iphost = Dns.GetHostEntry(Environment.MachineName);
            if (iphost.AddressList.Length>0)
            {
                foreach (var item in iphost.AddressList)
                {
                    string hostName = Dns.GetHostEntry(item.ToString()).HostName;
                    if (!this.cmb_ServerNode.Items.Contains(hostName))
                    {
                        this.cmb_ServerNode.Items.Add(hostName);
                    }
                }
            }
            else
            {
                return;
            }
        }
        /// <summary>
        /// 此方法连接OPCServer上的一个Program.
        /// </summary>
        /// <param name="host"></param>
        /// <param name="prog"></param>
        /// <returns></returns>
        public bool Connect(string host, string prog)
        {
            try
            {
                if (kepServer.ServerState == (int)OPCServerState.OPCRunning)
                {
                    if (this.kepServer != null)
                    {
                        this.kepServer.Disconnect();
                        //this.connected = false;
                    }
                }
                this.tvwGroupList.Nodes.Clear();
                this.groupMap.Clear();
                this.itemMap.Clear();
                this.kepServer.Connect(prog, host);
                this.tsslblServerState.Text = "OPC服务[" + prog + "]已连接";
                TreeNode node = this.tvwGroupList.Nodes.Add(prog);
                node.Tag = this.kepServer;
                this.opcBrowser = this.kepServer.CreateBrowser();
                LoadDataToTree(node, "");
                this.SetItemsClientHandle();
                //this.LoadConfig(prog);
                this.tsslblItemCount.Text = "Item Count " + this.itemMap.Count.ToString();
                this.tsslblGroupCount.Text = "Group Count " + this.groupMap.Count.ToString();
                //this.connected = true;
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
            return true;
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
                this.opcBrowser.ShowBranches();
                int count = this.opcBrowser.Count;
                if (count == 0)
                {
                    this.opcBrowser.ShowLeafs(true);
                    group = this.kepServer.OPCGroups.Add(nodeName);
                    group.UpdateRate = 1000;
                    group.IsSubscribed = true;
                    group.IsActive = true;
                    node.Tag = group;
                    this.groupMap.Add(nodeName, group);
                }
                HashSet<string> itemList = new HashSet<string>();
                foreach (object turn in this.opcBrowser)
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
                        this.opcBrowser.MoveDown(name);
                        LoadDataToTree(childNode, path);
                        this.opcBrowser.MoveUp();
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

                this.opcBrowser.ShowLeafs(false);
                List<string> leafItems = new List<string>();
                foreach (object t in this.opcBrowser)
                {
                    string itemId = this.opcBrowser.GetItemID(t.ToString());
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
                LogHelper.Error(ex);
            }
            return true;
        }

        private ListViewItem listitem;
        //此方法将选中的叶子节点(OPCGroup)下的所有OPCItem显示在ListView中.
        private void tvwGroupList_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {

                TreeNode crtNode = this.tvwGroupList.GetNodeAt(e.X, e.Y);
                TreeNode oldNode = this.tvwGroupList.SelectedNode;
                if (oldNode != null)
                {
                    this.lstviewItems.Items.Clear();
                    if (oldNode.Tag is OPCGroup)
                    {
                        OPCGroup old_group = oldNode.Tag as OPCGroup;
                    }
                }
                this.tvwGroupList.SelectedNode = crtNode;

                if (e.Button == MouseButtons.Right)
                {
                    if (crtNode.Tag is OPCServer)
                    {
                        this.opcServercontextMenuStrip.Show(this.tvwGroupList, new Point(e.X, e.Y));
                    }
                    else if (crtNode.Tag is OPCGroup)
                    {
                        this.opcGroupcontextMenuStrip.Show(this.tvwGroupList, new Point(e.X, e.Y));
                    }
                }
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
                    //OPCEnumHelper.GetQuality((OPCQuality)item.Quality),
                    //"0"
                            });
                            
                            listitem.Tag = item.ClientHandle;
                            this.lstviewItems.Items.Add(listitem);
                        }
                      
                    }
                    else
                    {
                        Array branches = crt_group.Name.Split('.');
                        this.opcBrowser.MoveTo(ref branches);
                        this.opcBrowser.ShowLeafs(true);
                        foreach (object turn in this.opcBrowser)
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
                            this.lstviewItems.Items.Add(listitem);
                        }

                        this.opcBrowser.ShowLeafs(true);
                        this.opcBrowser.MoveToRoot();
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            Connect("127.0.0.1", "KEPware.KEPServerEx.V4");
          
        }

        internal class LogHelper
        {
            public static void Error(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        private void lstviewItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbShow.Text = "";
            if (lstviewItems.SelectedIndices!=null && lstviewItems.SelectedIndices.Count>0)
            {
                var x = lstviewItems.SelectedIndices;
                foreach (var item in lstviewItems.Items[x[0]].SubItems)
                {                  
                    lbShow.Text += item.ToString() + "\r\n";
                }
               
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
            if (this.btn_Connect.Text==Enum.GetName(typeof(StatusHelper.ConnectHelper), StatusHelper.ConnectHelper.Connect))
            {
                try
                {
                    kepServer.Connect(this.cmb_ServerName.Text.Trim(), this.cmb_ServerNode.Text.Trim());

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
                kepGruop.UpdateRate = 500;
                kepGruop.AsyncReadComplete += KepGruop_AsyncReadComplete;

            }
            else
            {
                if (kepServer!=null)
                {
                    kepServer.Disconnect();
                    kepServer = null;
                    this.btn_Connect.Text = Enum.GetName(typeof(StatusHelper.ConnectHelper),StatusHelper.ConnectHelper.Connect);
                }
            }
        }

        private void KepGruop_AsyncReadComplete(int TransactionID, int NumItems, ref Array ClientHandles, ref Array ItemValues, ref Array Qualities, ref Array TimeStamps, ref Array Errors)
        {
            for (int i = 1; i <= NumItems; i++)
            {
                object value = ItemValues.GetValue(i);
               
                if (value!=null)
                {
                    this.OPCList[i - 1].Value = value.ToString();
                    this.OPCList[i - 1].Time = ((DateTime)TimeStamps.GetValue(1)).ToLocalTime();
                }
            }
        }
    }
}
