using System;
using System.Collections.Generic;
using System.Web;
using Johnson.Process.Core;

namespace Johnson.Process.Website.Models
{
    public class MaterialModel
    {
        public MaterialModel()
        {
        }

        public MaterialModel(Material material)
        {
            this.code = material.Code;
            this.quantity = material.Quantity;
            this.remark = material.Remark;
            this.sapNo = material.SapNo;
        }

        public string code;

        public string sapNo;

        public int quantity;

        public string remark;

        public Material Map()
        {
            return new Material 
            { 
                Code = this.code,
                Quantity = this.quantity,
                Remark = this.remark,
                SapNo = this.sapNo
            };
        }
    }
}