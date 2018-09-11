

using System;
using CodeTool.Common.Convert.CSharp2VB;

namespace ConvertCSharp2VB
{

	public class WhileManager:BaseManager
	{
		private string ExpresionToken = "";
		private string WhileBlockToken = "";
		private object oParent ;

		public string GetBlock(object toSender, string tcWhileCondition, string tcWhileBlock)
		{
			this.oParent = toSender;

			this.GetBlankToken(tcWhileCondition);
			this.GetCondition(tcWhileCondition);

			//remove the while condition from the start of the block
			tcWhileBlock = tcWhileBlock.Substring(tcWhileCondition.Length);
			this.WhileBlockToken = this.GetCurrentBlock(tcWhileBlock);

			return this.Execute();
		}

		private void GetCondition(string tcLine)
		{
			tcLine = ((ConvertCSharp2VB.CSharpToVBConverter)this.oParent).HandleCasting(tcLine);
			string lcStr = this.ExtractBlock(tcLine,"(", ")");
			lcStr = ReplaceManager.GetSingledSpacedString(lcStr);

			this.ExpresionToken = ReplaceManager.HandleExpression(lcStr);

		}

		private string Execute()
		{
			string cRetVal = "";
			cRetVal += this.BlankToken + "While " + this.ExpresionToken + "\n";
			cRetVal += this.WhileBlockToken;
			cRetVal += "\n" + this.BlankToken + "End While";

			return cRetVal;
		}
	}
}
