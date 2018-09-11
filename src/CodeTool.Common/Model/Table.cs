using System.Collections.Generic;

namespace CodeTool.Common.Model
{
    /// <summary>
    /// ���ݿ��
    /// </summary>
    public class Table
    {
        /// <summary>
        /// ����һ�������ݿ��
        /// </summary>
        public Table()
        {
            this.Fields = new List<Field>();
        }

      
        /// <summary>
        /// �������Ŀ�
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        public Database Database { get; set; }

        /// <summary>
        /// �������
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// �Ƿ�����ͼ
        /// </summary>
        public bool IsView { get; set; }

        /// <summary>
        /// ���е�������
        /// </summary>
        public List<global::CodeTool.Common.Model.Field> Fields { get; set; }
      
        /// <summary>
        /// ���һ���ֶ�
        /// </summary>
        public void AddField(global::CodeTool.Common.Model.Field field)
        {
            field.Table = this;
            Fields.Add(field);
        }

        /// <summary>
        /// ת��Ϊ�ַ���
        /// </summary>
        /// <returns>���ر���</returns>
        public override string ToString()
        {
            return this.Name;
        }
      
    }
}
