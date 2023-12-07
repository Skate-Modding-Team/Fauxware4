using FW4.pegasus;
using FW4.rw.core.arena;
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
            /*
            VersionData vdata = new VersionData();
            vdata.version = 25;
            vdata.revision = 13;

            K8.AssetConvert.ConvertPresArenaFile(openArenaDialog.FileName, vdata, FW4.ArenaSerialize.Platform.XB2);
           */

            Arena arena = FW4.ArenaSerialize.DeserializeArenaFile(openArenaDialog.FileName);
            FW4.ArenaSerialize.SerializeArena(arena, openArenaDialog.FileName + "\\Serialized\\");
        }
    }
}