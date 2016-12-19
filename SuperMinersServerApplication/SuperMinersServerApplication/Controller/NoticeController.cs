using MetaData;
using SuperMinersServerApplication.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SuperMinersServerApplication.Controller
{
    public class NoticeController
    {
        #region Single

        private static NoticeController _instance = new NoticeController();

        public static NoticeController Instance
        {
            get
            {
                return _instance;
            }
        }

        private NoticeController()
        {

        }

        #endregion

        private System.Threading.SynchronizationContext _syn = System.Threading.SynchronizationContext.Current;

        private ObservableCollection<NoticeInfo> _listNotices = new ObservableCollection<NoticeInfo>();

        public ObservableCollection<NoticeInfo> ListNotices
        {
            get
            {
                return this._listNotices;
            }
        }

        public void Init()
        {
            try
            {
                _listNotices.Clear();

                List<NoticeInfo> list = new List<NoticeInfo>();
                string[] noticeFiles = Directory.GetFiles(GlobalData.NoticeFolder);
                if (noticeFiles != null)
                {
                    foreach (var item in noticeFiles)
                    {
                        try
                        {
                            NoticeInfo notice = this.ReadFromXml(item);
                            list.Add(notice);
                        }
                        catch (Exception excsub)
                        {
                            LogHelper.Instance.AddErrorLog("Read Notice file " + item + " Error!", excsub);
                        }
                    }
                }

                foreach (var item in list.OrderByDescending(n => n.Time))
                {
                    _listNotices.Add(item);
                }
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("init NoticeController Error!", exc);
            }
        }

        public bool SaveNotice(NoticeInfo notice, bool isAdd)
        {
            try
            {
                //notice.Time = DateTime.Now;
                SaveToXml(Path.Combine(GlobalData.NoticeFolder, notice.FileName), notice);

                if (this._syn == System.Threading.SynchronizationContext.Current)
                {
                    Init();
                }
                else
                {
                    this._syn.Post(o =>
                    {
                        Init();
                    }, null);
                }
                if (NoticeChanged != null)
                {
                    NoticeChanged(notice.Title);
                }
                return true;
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("Create Notice " + notice.Title + " Error.", exc);
                return false;
            }
        }

        //public bool CreateNotice(NoticeInfo notice)
        //{
        //    try
        //    {
        //        notice.Time = DateTime.Now;
        //        SaveToXml(Path.Combine(GlobalData.NoticeFolder, notice.FileName), notice);

        //        if (this._syn == System.Threading.SynchronizationContext.Current)
        //        {
        //            this.ListNotices.Insert(0, notice);
        //        }
        //        else
        //        {
        //            this._syn.Post(o =>
        //            {
        //                this.ListNotices.Insert(0, notice);
        //            }, null);
        //        }
        //        if (NoticeAdded != null)
        //        {
        //            NoticeAdded(notice.Title);
        //        }
        //        return true;
        //    }
        //    catch (Exception exc)
        //    {
        //        LogHelper.Instance.AddErrorLog("Create Notice " + notice.Title + " Error.", exc);
        //        return false;
        //    }
        //}

        public bool DeleteNotice(NoticeInfo notice)
        {
            try
            {
                this._listNotices.Remove(notice);
                File.Delete(Path.Combine(GlobalData.NoticeFolder, notice.FileName));

                return true;
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("Delete Notice " + notice.Title + " Error!", exc);
                return false;
            }
        }

        public void SetAllChecked(bool isChecked)
        {
            foreach (var item in ListNotices)
            {
                item.Checked = isChecked;
            }
        }

        public NoticeInfo[] GetAllNotices()
        {
            return this.ListNotices.ToArray();
        }

        public List<NoticeInfo> GetCheckedNotices()
        {
            List<NoticeInfo> list = new List<NoticeInfo>();
            foreach (var item in ListNotices)
            {
                if (item.Checked)
                {
                    list.Add(item);
                }
            }

            return list;
        }

        private void SaveToXml(string path, NoticeInfo notice)
        {
            XmlDocument xmlDoc = new XmlDocument();
            //加入XML的声明段落,<?xml version="1.0" encoding="gb2312"?>
            XmlDeclaration xmlDeclar;
            xmlDeclar = xmlDoc.CreateXmlDeclaration("1.0", "gb2312", null);
            xmlDoc.AppendChild(xmlDeclar);

            //加入notice根元素
            XmlElement root = xmlDoc.CreateElement("Notice");
            //添加子节点
            XmlElement xeTitle = xmlDoc.CreateElement("Title");
            xeTitle.InnerText = notice.Title;
            root.AppendChild(xeTitle);

            XmlElement xeTime = xmlDoc.CreateElement("CreateTime");
            xeTime.InnerText = notice.TimeString;
            root.AppendChild(xeTime);

            XmlElement xeContent = xmlDoc.CreateElement("Content");
            xeContent.InnerText = notice.Content;
            root.AppendChild(xeContent);

            xmlDoc.AppendChild(root);
            xmlDoc.Save(path);//保存的路径
        }

        private NoticeInfo ReadFromXml(string path)
        {
            NoticeInfo notice = new NoticeInfo();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(path);

            XmlNode root = xmlDoc.SelectSingleNode("Notice");
            XmlNode nodeTitle = root.SelectSingleNode("Title");
            XmlNode nodeTime = root.SelectSingleNode("CreateTime");
            XmlNode nodeContent = root.SelectSingleNode("Content");

            notice.Title = nodeTitle.InnerText;
            notice.TimeString = nodeTime.InnerText;
            notice.Content = nodeContent.InnerText;
            notice.FileName = Path.GetFileNameWithoutExtension(path);

            return notice;
        }

        public event Action<string> NoticeChanged;
    }
}
