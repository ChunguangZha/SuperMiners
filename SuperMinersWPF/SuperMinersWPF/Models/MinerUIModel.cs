using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersWPF.Models
{
    /// <summary>
    /// 矿工类
    /// </summary>
    public class MinerUIModel
    {
        private DateTime createTime;

        public DateTime CreateTime
        {
            get { return createTime; }
            set { createTime = value; }
        }

        private float rateOfOutput;

        public float RateOfOutput
        {
            get { return rateOfOutput; }
            set { rateOfOutput = value; }
        }

    }
}
