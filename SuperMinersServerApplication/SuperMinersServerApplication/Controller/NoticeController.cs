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

                string[] noticeFiles = Directory.GetFiles(GlobalData.NoticeFolder);
                if (noticeFiles != null)
                {
                    foreach (var item in noticeFiles)
                    {
                        try
                        {
                            NoticeInfo notice = this.ReadFromXml(item);
                            _listNotices.Insert(0, notice);
                        }
                        catch (Exception excsub)
                        {
                            LogHelper.Instance.AddErrorLog("Read Notice file " + item + " Error!", excsub);
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("init NoticeController Error!", exc);
            }
        }

        public bool CreateNotice(NoticeInfo notice)
        {
            try
            {
                notice.Time = DateTime.Now;
                SaveToXml(Path.Combine(GlobalData.NoticeFolder, notice.FileName), notice);

                if (this._syn == System.Threading.SynchronizationContext.Current)
                {
                    this.ListNotices.Insert(0, notice);
                }
                else
                {
                    this._syn.Post(o =>
                    {
                        this.ListNotices.Insert(0, notice);
                    }, null);
                }
                if (NoticeAdded != null)
                {
                    NoticeAdded(notice.Title);
                }
                return true;
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("Create Notice " + notice.Title + " Error.", exc);
                return false;
            }
        }

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
                item.IsChecked = isChecked;
            }
        }

        public NoticeInfo[] GetAllNotices()
        {
            return this.ListNotices.ToArray();
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

            return notice;
        }

        public event Action<string> NoticeAdded;
    }
}
