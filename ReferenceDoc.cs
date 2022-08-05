namespace FF6Hack
{
	using System.IO;
	using System.Text;


	public class ReferenceDoc
	{
		private string path;
		private StringBuilder fileContents;

		public ReferenceDoc(string path, string header = null)
		{
			this.path = path;
			this.fileContents = new StringBuilder();
			if (header != null)
				AddHeader(header);
		}


		public ReferenceDoc AddText(string text)
		{
			this.fileContents.Append(text);
			return this;
		}


		public void WriteFile()
		{
			File.WriteAllText(this.path, this.fileContents.ToString());
		}


		private void AddHeader(string header)
		{
			this.fileContents.AppendLine(header);
			for (int i = 0; i < header.Length; i++)
			{
				this.fileContents.Append("=");
			}
			this.fileContents.AppendLine();
		}
	}
}
