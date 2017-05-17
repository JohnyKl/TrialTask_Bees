using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TrialTask_Bees
{
    public partial class Default : System.Web.UI.Page
    {        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
                StartGame();
            else
            {
                if (ViewState["game"] != null)
                {
                    game = (BeesGame)ViewState["game"];
                }
            }
        }

        void Page_PreRender(object sender, EventArgs e)
        {
            ViewState.Add("game", game);
        }

        protected void btnHit_Click(object sender, EventArgs e)
        {
            game.HitRandomBee();
            lblHitCounter.Text = string.Format(hitCounterLabelFormat, game.HitCount, game.GetTotalKilled());
            tbBeesInfo.Text = game.AliveBeesToString();

            if(game.IsGameOver())
            {
                StartGame();
            }

            ViewState.Add("game", game);
        }

        protected void btnStart_Click(object sender, EventArgs e)
        {
            StartGame();
        }      

        protected void tb_TextChanged(object sender, EventArgs e) { }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string directoryPath = Server.MapPath("~/") + "savedFiles";
            if(!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            game.Save(directoryPath + "/bees_game{0}.xml");
        }

        private void StartGame()
        {
            if(game == null)
                game = new BeesGame();
            
            game.Restart();

            CreateBeesByTypes(Bee.BeeTypes.DroneBee, tbDroneBeeNumber, tbDroneBeeHealth, tbDroneBeeHitPoints);
            CreateBeesByTypes(Bee.BeeTypes.WorkerBee, tbWorkerBeeNumber, tbWorkerBeeHealth, tbWorkerBeeHitPoints);
            CreateBeesByTypes(Bee.BeeTypes.QueenBee, tbQueenBeeNumber, tbQueenBeeHealth, tbQueenBeeHitPoints);

            game.Bees.Reverse();

            tbBeesInfo.Text = game.AliveBeesToString();

            btnHit.Enabled = true;
        }

        private void CreateBeesByTypes(Bee.BeeTypes type, TextBox tbNumber, TextBox tbHealth, TextBox tbHitPoints)
        {
            try
            {
                int number = int.Parse(tbNumber.Text);
                int health = int.Parse(tbHealth.Text);
                int hitPoints = int.Parse(tbHitPoints.Text);

                game.CreateBee(type, number, health, hitPoints);
            }
            catch(Exception ex)
            {
                lblMessage.Text = "Message: " + ex.Message;
            }
        }

        private static string hitCounterLabelFormat = "Current Hits: {0}; Total killed: {1}";
        private BeesGame game = null;        
    }
}