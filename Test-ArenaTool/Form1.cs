using System.Text.Json;

namespace Test_ArenaTool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openArenaDialog.ShowDialog();
        }

        private void openArenaDialog_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            FW4.rw.core.arena.Arena arena = FW4.ArenaSerialize.DeserializeArenaFile(openArenaDialog.FileName);
            String json = JsonSerializer.Serialize(arena);
            richTextBox1.Text = json;
        }
    }
}