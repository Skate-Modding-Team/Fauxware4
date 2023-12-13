using FW4.Pegasus;
using FW4.RW.Core.Arena;
using FW4.Serialization;
using System.Text.Json;

namespace Test_ArenaTool
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //openArenaDialog.ShowDialog();
            VersionData vdata = new VersionData();
            vdata.version = 25;
            vdata.revision = 13;
            RWSerializer ser = new RWSerializer();
            byte[] testdata = ser.Serialize(vdata, true);

            int x = 1;

        }

        private void openArenaDialog_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            /*
            VersionData vdata = new VersionData();
            vdata.version = 25;
            vdata.revision = 13;

            K8.AssetConvert.ConvertPresArenaFile(openArenaDialog.FileName, vdata, FW4.ArenaSerialize.Platform.XB2);
           */

            Arena Arena = FW4.ArenaSerialize.DeserializeArenaFile(openArenaDialog.FileName);
            FW4.ArenaSerialize.SerializeArena(Arena, openArenaDialog.FileName + ".Arena");
        }
    }
}