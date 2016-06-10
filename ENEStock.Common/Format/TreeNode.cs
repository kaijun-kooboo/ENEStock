using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ENEStock.Common.Format
{
    /// <summary>
    /// EasyUI的tree组件 的 节点 模型
    /// </summary>
    public class TreeNode
    {
        /// <summary>
        /// 节点id
        /// </summary>
        public int id;

        /// <summary>
        /// 节点显示文本
        /// </summary>
        public string text;

        /// <summary>
        /// 节点状态 "open"  "closed"
        /// </summary>
        public string state="open";

        /// <summary>
        /// 节点是否被选中（此节点只有为复选框树的节点才有用）
        /// </summary>
        public bool Checked;

        /// <summary>
        /// 自定义数据（可在被点击的时候获取）
        /// </summary>
        public object attributes;

        /// <summary>
        /// 子节点集合
        /// </summary>
        public List<TreeNode> children;
    }
}
