/************************************************************************ 
 * 项目名称 ：RFIDUI
 * 项目描述 ：
 * 文件名称 ：Main.cs
 * 版 本 号 ：v1.0.0.0
 * 说    明 ：
 * 作    者 ：WUZE
 * IDE 环境 ：Visual Studio 2013
 * 创建时间 ：2018/7/9 15:26:51
 * 更新时间 ：2018/7/9 15:26:51
************************************************************************
 * Copyright @ Njbosa 2018. All rights reserved.
************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using JW;
using JW.UHF;
using System.Threading;
using System.Collections;

namespace RFIDUI
{
    public partial class Main : Form
    {
        JWReader jwReader;
        Result result;
        Queue<Tag> inventoryTagQueue = new Queue<Tag>();
        //Stack st = new Stack();
        BindingList<TagProperty> list = new BindingList<TagProperty>();
        bool isSet = false, inventory = false;
        Thread th, th_temp;
        private object lockObj = new object();//线程同步锁

        public Main()
        {
            InitializeComponent();
            dg_data.DataSource = list;
        }

        private void btn_open_Click(object sender, EventArgs e)
        {
            txt_ip.ReadOnly = true;
            txt_port.ReadOnly = true;
            btn_open.Enabled = false;
            if (btn_open.Text == "打开")
            {
                string ip = txt_ip.Text;
                int port = Convert.ToInt32(txt_port.Text);
                FirmwareInfo firmwareInfo = null;
                jwReader = new JWReader(ip, port);
                result = jwReader.RFID_Open(false);
                RfidSetting rs = null;
                result = jwReader.RFID_Get_Config(out rs);
                if (result != Result.OK)
                {
                    MessageBox.Show("打开失败！" + result.ToString());
                    btn_open.Enabled = true;
                    txt_ip.ReadOnly = false;
                    txt_port.ReadOnly = false;
                    return;
                }
                result = jwReader.RFID_Get_Firmware_Info(out firmwareInfo);
                if (result != Result.OK)
                {
                    MessageBox.Show("获取模块信息失败！");
                }
                if (firmwareInfo != null)
                {
                    this.Text = "RFID测试  " + "  Firmware：" + firmwareInfo.Main_Version + "." + firmwareInfo.Sub_Version;
                }

                btn_inventory.Enabled = true;
                if (!isSet)
                {
                    RFID_Init();
                    isSet = true;
                }
                btn_open.Text = "关闭";
                th_temp = new Thread(new ThreadStart(REID_Temperature));
                th_temp.Start();
            }
            else
            {
                if (inventory)
                    Stop_Inventory();
                if (th_temp != null)
                    th_temp.Abort();
                bool success = RFID_Close();
                if (!success)
                {
                    btn_open.Enabled = true;
                    return;
                }
                //txt_temperature.Text = "";
                this.Text = "RFID测试";
                btn_open.Text = "打开";
                btn_inventory.Enabled = false;
                txt_ip.ReadOnly = false;
                txt_port.ReadOnly = false;
            }
            btn_open.Enabled = true;
        }

        private void btn_inventory_Click(object sender, EventArgs e)
        {
            btn_inventory.Enabled = false;
            if (btn_inventory.Text == "打开盘点")
            {
                inventory = true;
                jwReader.TagsReported += TagsReport;
                jwReader.RFID_Start_Inventory(true);
                Thread th = new Thread(new ThreadStart(REID_Reader));
                th.Start();
                btn_inventory.Text = "关闭盘点";
            }
            else
            {
                inventory = false;
                bool stoped = jwReader.RFID_Stop_Inventory() == Result.OK;
                if (th != null)
                {
                    th.Join();
                }
                if (stoped)
                    btn_inventory.Text = "打开盘点";
            }
            btn_inventory.Enabled = true;
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (inventory)
                Stop_Inventory();

            if (th_temp != null)
                th_temp.Abort();
            RFID_Close();
        }

        private void TagsReport(object sender, TagsEventArgs args)
        {
            Tag tag = args.tag;
            lock (lockObj)
                inventoryTagQueue.Enqueue(tag);
        }

        private void RFID_Init()
        {
            //result = jwReader.RFID_Set_Inventory_Mode(InventoryMode.Continue); // 设置读写器为持续盘点模式
            //result = jwReader.RFID_Set_Profile(1);                             // 射频参数相关 一般默认即可
            RfidSetting rs = new RfidSetting();
            rs.AntennaPort_List = new List<AntennaPort>();
            AntennaPort ap = new AntennaPort();
            ap.AntennaIndex = 0;//天线1 
            ap.Power = 22;//功率设为27 
            rs.AntennaPort_List.Add(ap);
            rs.GPIO_Config = null;
            rs.Inventory_Time = 0;
            rs.Inventory_Mode = InventoryMode.Continue;
            rs.Region_List = RegionList.CCC;
            rs.RSSI_Filter = new RSSIFilter();
            rs.RSSI_Filter.Enable = true;
            rs.RSSI_Filter.RSSIValue = (float)-70;
            rs.Speed_Mode = SpeedMode.SPEED_FASTEST;
            rs.Tag_Group = new TagGroup();
            rs.Tag_Group.SessionTarget = SessionTarget.A;
            rs.Tag_Group.SearchMode = SearchMode.DUAL_TARGET;
            rs.Tag_Group.Session = Session.S0;
            result = jwReader.RFID_Set_Config(rs);
            if (result != Result.OK)
            {
                MessageBox.Show("配置模块错误！");
            }
        }

        private bool RFID_Close()
        {
            inventory = false;
            if (jwReader != null && jwReader.IsConnected)
            {
                result = jwReader.RFID_Close();
                if (result != Result.OK && jwReader.IsConnected)
                {
                    MessageBox.Show("关闭失败！" + result.ToString());
                    return false;
                }
                if (th != null)
                {
                    th.Join();
                }
            }
            return true;
        }

        private bool Stop_Inventory()
        {
            inventory = false;
            result = jwReader.RFID_Stop_Inventory();
            if (result != Result.OK)
            {
                MessageBox.Show("关闭盘点失败！" + result.ToString());
                return false;
            }
            jwReader.TagsReported -= TagsReport;
            btn_inventory.Text = "打开盘点";
            return true;
        }

        delegate void SetDataSourceCallBack(DataGridView dg, Tag tag);
        private void SetDataSource(DataGridView dg, Tag tag)
        {
            if (!inventory) return;
            if (this.InvokeRequired)
            {
                SetDataSourceCallBack stcb = new SetDataSourceCallBack(SetDataSource);
                if (!this.IsDisposed)
                    this.Invoke(stcb, new object[] { dg, tag });
            }
            else
            {
                TagProperty temp = list.FirstOrDefault(t => t.EPC == tag.EPC);
                if (temp != null)
                {
                    int index = list.IndexOf(temp);
                    list[index].RSSI = tag.RSSI;
                    list[index].Data = tag.DATA;
                    list[index].Time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    dg.InvalidateRow(index);
                }
                else
                {
                    TagProperty tp = new TagProperty()
                    {
                        Data = tag.DATA,
                        Port = tag.PORT,
                        EPC = tag.EPC,
                        RSSI = tag.RSSI,
                        Time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                    };
                    list.Add(tp);
                }
            }
        }

        delegate void SetTextCallBack(TextBox tb, string text);
        private void SetText(TextBox tb, string text)
        {
            if (this.InvokeRequired)
            {
                SetTextCallBack stcb = new SetTextCallBack(SetText);
                if (!this.IsDisposed)
                    this.Invoke(stcb, new object[] { tb, text });
            }
            else
            {
                tb.Text = text;
            }
        }

        private void REID_Reader()
        {
            while (inventory)
            {
                while (inventoryTagQueue.Count > 0)
                {
                    Tag packet = null;
                    lock (lockObj)
                        packet = inventoryTagQueue.Dequeue();
                    SetText(txt_count, inventoryTagQueue.Count.ToString());
                    SetDataSource(dg_data, packet);
                }
                Thread.Sleep(100);
            }
        }

        private void REID_Temperature()
        {
            while (true)
            {
                if (btn_inventory.Text != "打开盘点" || btn_open.Text != "关闭")
                    continue;
                int temperature = 0;
                result = jwReader.RFID_Get_Temperature(out temperature);
                if (result == Result.OK)
                {
                    SetText(txt_temperature, temperature.ToString());
                }
            }
        }
    }

    public class TagProperty
    {
        public string Data { get; set; }
        public string EPC { get; set; }
        public int Port { get; set; }
        public float RSSI { get; set; }
        public string Time { get; set; }
    }
}
