using System;
using System.Collections.Generic;
using OPCAutomation;

namespace KepCom
{ 
        public class OPC
        {
            #region 变量区
            //定义OPC服务器
            OPCServer opcServer = new OPCServer();
            //定义OPC服务器组集
            OPCGroups opcGruops;
            //定义OPC服务器组
            OPCGroup opcGruop;
            //定义OPC服务器项目集
            OPCItems opcItems;
            //定义OPC服务器浏览器
            OPCBrowser opcBrowser;
            //定义OPC变量集合
            public List<OpcHelperItem> OPCList = new List<OpcHelperItem>();
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

            public void Connect(string ServerName, string ServerNode)
            {
                //连接选择的OPC服务器
                opcServer.Connect(ServerName, ServerNode);

                //kepGruops对象赋值 
                opcGruops = opcServer.OPCGroups;
                //kepGruops属性设置
                opcGruops.DefaultGroupDeadband = 0;
                opcGruops.DefaultGroupIsActive = true;

                opcGruop = opcGruops.Add("group1");
                opcGruop.IsActive = true;
                opcGruop.IsSubscribed = true;
                opcGruop.UpdateRate = 250;
                opcGruop.AsyncReadComplete += opcGruop_AsyncReadComplete;

                //opcBrowser = opcServer.CreateBrowser();
                //opcBrowser.ShowBranches();
                //opcBrowser.ShowLeafs(true);

                //foreach (var item in opcBrowser)
                //{

                //}

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

                opcItems = opcGruop.OPCItems;
                opcItems.AddItems(this.OPCList.Count, ref strTempIDs, ref strClientHandles, out strServerHandles, out iErrors);
                serverHandles.Clear();
                serverHandles.Add(0);
                for (int i = 0; i < count; i++)
                {
                    serverHandles.Add(Convert.ToInt32(strServerHandles.GetValue(i + 1)));

                }
                readServerHandles = (Array)serverHandles.ToArray();
            }

            void opcGruop_AsyncReadComplete(int TransactionID, int NumItems, ref Array ClientHandles, ref Array ItemValues, ref Array Qualities, ref Array TimeStamps, ref Array Errors)
            {
                for (int i = 1; i <= NumItems; i++)
                {
                    object value = ItemValues.GetValue(i);

                    if (value != null)
                    {
                        this.OPCList[i - 1].Value = value.ToString();
                        this.OPCList[i - 1].Time = ((DateTime) TimeStamps.GetValue(1)).ToShortTimeString();
                    }
                }
            }

            public void ReadOPCItems()
            {
                if (this.OPCList.Count > 0)
                {
                    if (opcServer != null)
                    {
                        opcGruop.AsyncRead(this.OPCList.Count, ref readServerHandles, out readError, readTransID, out readCancelID);
                    }
                }
            }

            public void WriteOPCItems(int index, object value)
            {
                OpcHelperItem objItem = this.OPCList[index];
                int[] serverHandle = new int[] { 0, Convert.ToInt32(strServerHandles.GetValue(index + 1)) };
                object[] Values = new object[2];
                writeServerHandles = (Array)serverHandle;
                Values[1] = value;
                writeArrayHandles = (Array)Values;
                opcGruop.AsyncWrite(1, ref writeServerHandles, ref writeArrayHandles, out writeError, writeTransID, out writeCancelID);
            }
        }
    }