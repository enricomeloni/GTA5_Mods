using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rage;
using Rage.Forms;

namespace FirstPlugin
{
    class TestForm : GwenForm
    {
        private Gwen.Control.Button button1;
        private Gwen.Control.Label label1;

        public TestForm() : base(typeof(FormTemplate))
        {

        }

        public override void InitializeLayout()
        {
            base.InitializeLayout();

            label1.Text = "Inizializzato";

            button1.Clicked += button1_click;
        }

        private void button1_click(Gwen.Control.Base sender, Gwen.Control.ClickedEventArgs e)
        {
            label1.Text = "Cliccato";

            GameFiber.StartNew(delegate
            {
                Model workModel = new Model("s_m_y_airworker");
                Vector3 playerPosition = Game.LocalPlayer.Character.Position;
                Vector3 offset = new Vector3(0, 0, 0);
                Ped ped = new Ped(workModel, playerPosition + offset, 0);
            });

            this.Window.Close();
        }
    }
}
