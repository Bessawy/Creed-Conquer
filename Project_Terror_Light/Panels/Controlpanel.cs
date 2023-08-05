using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//  using SubSonic;


namespace Project_Terror_Light
{
    public partial class Controlpanel : Form
    {
        public Controlpanel()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Client.GameClient client = null;
            client = Client.GameClient.CharacterFromName(comboBox1.Text);
            if (client == null)
                return;
            CPs.Text = client.Player.ConquerPoints.ToString();
            Money.Text = client.Player.Money.ToString();
            Level.Text = client.Player.Level.ToString();
            str.Text = client.Player.Strength.ToString();
            vit.Text = client.Player.Vitality.ToString();
            agi.Text = client.Player.Agility.ToString();
            spi.Text = client.Player.Spirit.ToString();
            textBox4.Text = client.Player.SecurityPassword.ToString();
            textBox2.Text = client.Player.VipLevel.ToString();
            switch (client.Player.Class)
            {
                #region Get Class
                case 10:
                    {
                        Class.Text = "InrernTrojan";
                        break;
                    }
                case 11:
                    {
                        Class.Text = "Trojan";
                        break;
                    }
                case 12:
                    {
                        Class.Text = "VeteranTrojan";
                        break;
                    }
                case 13:
                    {
                        Class.Text = "TigerTrojan";
                        break;
                    }
                case 14:
                    {
                        Class.Text = "DragonTrojan";
                        break;
                    }
                case 15:
                    {
                        Class.Text = "TrojanMaster";
                        break;
                    }
                case 20:
                    {
                        Class.Text = "InrernWarrior";
                        break;
                    }
                case 21:
                    {
                        Class.Text = "Warrior";
                        break;
                    }
                case 22:
                    {
                        Class.Text = "BrassWarrior";
                        break;
                    }
                case 23:
                    {
                        Class.Text = "SilverWarrior";
                        break;
                    }
                case 24:
                    {
                        Class.Text = "GoldWarrior";
                        break;
                    }
                case 25:
                    {
                        Class.Text = "WarriorMaster";
                        break;
                    }
                case 40:
                    {
                        Class.Text = "InrernArcher";
                        break;
                    }
                case 41:
                    {
                        Class.Text = "Archer";
                        break;
                    }
                case 42:
                    {
                        Class.Text = "EagleArcher";
                        break;
                    }
                case 43:
                    {
                        Class.Text = "TigerArcher";
                        break;
                    }
                case 44:
                    {
                        Class.Text = "DragonArcher";
                        break;
                    }
                case 45:
                    {
                        Class.Text = "ArcherMaster";
                        break;
                    }
                case 50:
                    {
                        Class.Text = "InrernNinja";
                        break;
                    }
                case 51:
                    {
                        Class.Text = "Ninja";
                        break;
                    }
                case 52:
                    {
                        Class.Text = "MiddleNinja";
                        break;
                    }
                case 53:
                    {
                        Class.Text = "DarkNinja";
                        break;
                    }
                case 54:
                    {
                        Class.Text = "MysticNinja";
                        break;
                    }
                case 55:
                    {
                        Class.Text = "NinjaMaster";
                        break;
                    }
                case 60:
                    {
                        if (client.Player.Body == 1003 || client.Player.Body == 1004)
                            Class.Text = "InrernMonk";
                        else
                            Class.Text = "InrernSaint";
                        break;
                    }
                case 61:
                    {
                        if (client.Player.Body == 1003 || client.Player.Body == 1004)
                            Class.Text = "Monk";
                        else
                            Class.Text = "Saint";
                        break;
                    }
                case 62:
                    {
                        if (client.Player.Body == 1003 || client.Player.Body == 1004)
                            Class.Text = "DhyanaMonk";
                        else
                            Class.Text = "DhyanaSaint";
                        break;
                    }
                case 63:
                    {
                        if (client.Player.Body == 1003 || client.Player.Body == 1004)
                            Class.Text = "DharnaMonk";
                        else
                            Class.Text = "DharnaSaint";
                        break;
                    }
                case 64:
                    {
                        if (client.Player.Body == 1003 || client.Player.Body == 1004)
                            Class.Text = "PrajnaMonk";
                        else
                            Class.Text = "PrajnaSaint";
                        break;
                    }
                case 65:
                    {
                        if (client.Player.Body == 1003 || client.Player.Body == 1004)
                            Class.Text = "NirvanaMonk";
                        else
                            Class.Text = "NirvanaSaint";
                        break;
                    }
                case 70:
                    {
                        if (client.Player.Body == 1003 || client.Player.Body == 1004)
                            Class.Text = "InternMalePirate";
                        else
                            Class.Text = "InternFemalPirate";
                        break;
                    }
                case 71:
                    {
                        if (client.Player.Body == 1003 || client.Player.Body == 1004)
                            Class.Text = "MalePirate";
                        else
                            Class.Text = "FemalePirate";
                        break;
                    }
                case 72:
                    {
                        Class.Text = "PirateGunner";
                        break;
                    }
                case 73:
                    {
                        Class.Text = "Quartermaster";
                        break;
                    }
                case 74:
                    {
                        Class.Text = "PirateCaptain";
                        break;
                    }
                case 75:
                    {
                        Class.Text = "PirateLord";
                        break;
                    }
                case 80:
                    {
                        Class.Text = "Novice Dragon-Warrior";
                        break;
                    }
                case 81:
                    {
                        Class.Text = "Dragon-Warroir";
                        break;
                    }
                case 82:
                    {
                        Class.Text = "Expert Dragon-Warrior";
                        break;
                    }
                case 83:
                    {
                        Class.Text = "Elite Dragon-Warrior";
                        break;
                    }
                case 84:
                    {
                        Class.Text = "Master Dragon-Warrior";
                        break;
                    }
                case 85:
                    {
                        Class.Text = "King Dragon-Warrior";
                        break;
                    }
                case 100:
                    {
                        Class.Text = "InternTaoist";
                        break;
                    }
                case 101:
                    {
                        Class.Text = "Taoist";
                        break;
                    }
                case 132:
                    {
                        Class.Text = "WaterTaoist";
                        break;
                    }
                case 133:
                    {
                        Class.Text = "WaterWizard";
                        break;
                    }
                case 134:
                    {
                        Class.Text = "WaterMaster";
                        break;
                    }
                case 135:
                    {
                        Class.Text = "WaterSaint";
                        break;
                    }
                case 142:
                    {
                        Class.Text = "FireTaoist";
                        break;
                    }
                case 143:
                    {
                        Class.Text = "FireWizard";
                        break;
                    }
                case 144:
                    {
                        Class.Text = "FireMaster";
                        break;
                    }
                case 145:
                    {
                        Class.Text = "FireSaint";
                        break;
                    }
                case 160:
                    {
                        Class.Text = "Windwalker";
                        break;
                    }
                case 161:
                    {
                        Class.Text = "Wind.Guard";
                        break;
                    }
                case 162:
                    {
                        Class.Text = "Wind.Officer";
                        break;
                    }
                case 163:
                    {
                        Class.Text = "Wind.Supervisor";
                        break;
                    }
                case 164:
                    {
                        Class.Text = "Wind.Maneger";
                        break;
                    }
                case 165:
                    {
                        Class.Text = "Wind.Lord";
                        break;
                    }
                default: Class.Text = "UnKowen"; break;
                #endregion
            }
            switch (client.Player.Body)
            {
                case 1003: sex.Text = "Male(S)"; break;
                case 1004: sex.Text = "Male(L)"; break;
                case 2001: sex.Text = "Female(S)"; break;
                case 2002: sex.Text = "Female(L)"; break;
            }
            switch (client.Player.Reborn)
            {
                case 2: Reborn.Text = "2nd Reborn"; break;
                case 1: Reborn.Text = "1st Reborn"; break;
                default: Reborn.Text = "Nono"; break;
            }
            double x = 0;
            if ((client.Player.ExpireVip > DateTime.Now))
            {
                x = (client.Player.ExpireVip - DateTime.Now).TotalDays;
            }
            textBox3.Text = x.ToString();
            textBox2.Text = client.Player.VipLevel.ToString();

        }
        private void button3_Click(object sender, EventArgs e)
        {
            Client.GameClient client = null;
            client = Client.GameClient.CharacterFromName(comboBox1.Text);
            if (client == null)
                return;
            client.Socket.Disconnect();

        }
        private void button1_Click_2(object sender, EventArgs e)
        {
            Client.GameClient client = null;
            client = Client.GameClient.CharacterFromName(comboBox1.Text);
            if (client == null)
                return;
            client.Player.ConquerPoints = uint.Parse(CPs.Text);
            client.Player.Money = long.Parse(Money.Text);
            client.Player.Strength = ushort.Parse(str.Text);
             client.Player.Vitality = ushort.Parse(vit.Text);
             client.Player.Agility = ushort.Parse(agi.Text);
             client.Player.Spirit = ushort.Parse(spi.Text);
            client.Player.ExpireVip = DateTime.Now.AddDays(double.Parse(textBox3.Text));
            client.Player.VipLevel = byte.Parse(textBox2.Text);
            client.Player.SecurityPassword = uint.Parse(textBox4.Text);
            using (var rec = new ServerSockets.RecycledPacket())
            {
                var stream = rec.GetStream();
                client.Player.SendUpdate(stream, client.Player.VipLevel, Game.MsgServer.MsgUpdate.DataType.VIPLevel);
                client.Player.UpdateVip(stream);
                client.UpdateLevel(stream, byte.Parse(Level.Text));
            }
        }
        private void Control_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            foreach (var user in Database.Server.GamePoll.Values)
            {
                comboBox1.Items.Add(user.Player.Name);
            }
            foreach (var item in Database.Server.ItemsBase.Values)
            {
                comboBox3.Items.Add(item.Name + " " + item.ID);
            }
            foreach (var ban in Database.SystemBannedAccount.BannedPoll.Values)
            {
                comboBox2.Items.Add(ban.Name);
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Client.GameClient client = null;
            client = Client.GameClient.CharacterFromName(comboBox1.Text);
            if (client == null)
                return;
            CPs.Text = client.Player.ConquerPoints.ToString();
            Money.Text = client.Player.Money.ToString();
            Level.Text = client.Player.Level.ToString();
            str.Text = client.Player.Strength.ToString();
            vit.Text = client.Player.Vitality.ToString();
            agi.Text = client.Player.Agility.ToString();
            spi.Text = client.Player.Spirit.ToString();
            textBox4.Text = client.Player.SecurityPassword.ToString();
            textBox2.Text = client.Player.VipLevel.ToString();
            switch (client.Player.Class)
            {
                #region Get Class
                case 10:
                    {
                        Class.Text = "InrernTrojan";
                        break;
                    }
                case 11:
                    {
                        Class.Text = "Trojan";
                        break;
                    }
                case 12:
                    {
                        Class.Text = "VeteranTrojan";
                        break;
                    }
                case 13:
                    {
                        Class.Text = "TigerTrojan";
                        break;
                    }
                case 14:
                    {
                        Class.Text = "DragonTrojan";
                        break;
                    }
                case 15:
                    {
                        Class.Text = "TrojanMaster";
                        break;
                    }
                case 20:
                    {
                        Class.Text = "InrernWarrior";
                        break;
                    }
                case 21:
                    {
                        Class.Text = "Warrior";
                        break;
                    }
                case 22:
                    {
                        Class.Text = "BrassWarrior";
                        break;
                    }
                case 23:
                    {
                        Class.Text = "SilverWarrior";
                        break;
                    }
                case 24:
                    {
                        Class.Text = "GoldWarrior";
                        break;
                    }
                case 25:
                    {
                        Class.Text = "WarriorMaster";
                        break;
                    }
                case 40:
                    {
                        Class.Text = "InrernArcher";
                        break;
                    }
                case 41:
                    {
                        Class.Text = "Archer";
                        break;
                    }
                case 42:
                    {
                        Class.Text = "EagleArcher";
                        break;
                    }
                case 43:
                    {
                        Class.Text = "TigerArcher";
                        break;
                    }
                case 44:
                    {
                        Class.Text = "DragonArcher";
                        break;
                    }
                case 45:
                    {
                        Class.Text = "ArcherMaster";
                        break;
                    }
                case 50:
                    {
                        Class.Text = "InrernNinja";
                        break;
                    }
                case 51:
                    {
                        Class.Text = "Ninja";
                        break;
                    }
                case 52:
                    {
                        Class.Text = "MiddleNinja";
                        break;
                    }
                case 53:
                    {
                        Class.Text = "DarkNinja";
                        break;
                    }
                case 54:
                    {
                        Class.Text = "MysticNinja";
                        break;
                    }
                case 55:
                    {
                        Class.Text = "NinjaMaster";
                        break;
                    }
                case 60:
                    {
                        if (client.Player.Body == 1003 || client.Player.Body == 1004)
                            Class.Text = "InrernMonk";
                        else
                            Class.Text = "InrernSaint";
                        break;
                    }
                case 61:
                    {
                        if (client.Player.Body == 1003 || client.Player.Body == 1004)
                            Class.Text = "Monk";
                        else
                            Class.Text = "Saint";
                        break;
                    }
                case 62:
                    {
                        if (client.Player.Body == 1003 || client.Player.Body == 1004)
                            Class.Text = "DhyanaMonk";
                        else
                            Class.Text = "DhyanaSaint";
                        break;
                    }
                case 63:
                    {
                        if (client.Player.Body == 1003 || client.Player.Body == 1004)
                            Class.Text = "DharnaMonk";
                        else
                            Class.Text = "DharnaSaint";
                        break;
                    }
                case 64:
                    {
                        if (client.Player.Body == 1003 || client.Player.Body == 1004)
                            Class.Text = "PrajnaMonk";
                        else
                            Class.Text = "PrajnaSaint";
                        break;
                    }
                case 65:
                    {
                        if (client.Player.Body == 1003 || client.Player.Body == 1004)
                            Class.Text = "NirvanaMonk";
                        else
                            Class.Text = "NirvanaSaint";
                        break;
                    }
                case 70:
                    {
                        if (client.Player.Body == 1003 || client.Player.Body == 1004)
                            Class.Text = "InternMalePirate";
                        else
                            Class.Text = "InternFemalPirate";
                        break;
                    }
                case 71:
                    {
                        if (client.Player.Body == 1003 || client.Player.Body == 1004)
                            Class.Text = "MalePirate";
                        else
                            Class.Text = "FemalePirate";
                        break;
                    }
                case 72:
                    {
                        Class.Text = "PirateGunner";
                        break;
                    }
                case 73:
                    {
                        Class.Text = "Quartermaster";
                        break;
                    }
                case 74:
                    {
                        Class.Text = "PirateCaptain";
                        break;
                    }
                case 75:
                    {
                        Class.Text = "PirateLord";
                        break;
                    }
                case 80:
                    {
                        Class.Text = "Novice Dragon-Warrior";
                        break;
                    }
                case 81:
                    {
                        Class.Text = "Dragon-Warroir";
                        break;
                    }
                case 82:
                    {
                        Class.Text = "Expert Dragon-Warrior";
                        break;
                    }
                case 83:
                    {
                        Class.Text = "Elite Dragon-Warrior";
                        break;
                    }
                case 84:
                    {
                        Class.Text = "Master Dragon-Warrior";
                        break;
                    }
                case 85:
                    {
                        Class.Text = "King Dragon-Warrior";
                        break;
                    }
                case 100:
                    {
                        Class.Text = "InternTaoist";
                        break;
                    }
                case 101:
                    {
                        Class.Text = "Taoist";
                        break;
                    }
                case 132:
                    {
                        Class.Text = "WaterTaoist";
                        break;
                    }
                case 133:
                    {
                        Class.Text = "WaterWizard";
                        break;
                    }
                case 134:
                    {
                        Class.Text = "WaterMaster";
                        break;
                    }
                case 135:
                    {
                        Class.Text = "WaterSaint";
                        break;
                    }
                case 142:
                    {
                        Class.Text = "FireTaoist";
                        break;
                    }
                case 143:
                    {
                        Class.Text = "FireWizard";
                        break;
                    }
                case 144:
                    {
                        Class.Text = "FireMaster";
                        break;
                    }
                case 145:
                    {
                        Class.Text = "FireSaint";
                        break;
                    }
                case 160:
                    {
                        Class.Text = "Windwalker";
                        break;
                    }
                case 161:
                    {
                        Class.Text = "Wind.Guard";
                        break;
                    }
                case 162:
                    {
                        Class.Text = "Wind.Officer";
                        break;
                    }
                case 163:
                    {
                        Class.Text = "Wind.Supervisor";
                        break;
                    }
                case 164:
                    {
                        Class.Text = "Wind.Maneger";
                        break;
                    }
                case 165:
                    {
                        Class.Text = "Wind.Lord";
                        break;
                    }
                default: Class.Text = "UnKowen"; break;
                #endregion
            }
            switch (client.Player.Body)
            {
                case 1003: sex.Text = "Male(S)"; break;
                case 1004: sex.Text = "Male(L)"; break;
                case 2001: sex.Text = "Female(S)"; break;
                case 2002: sex.Text = "Female(L)"; break;
            }
            switch (client.Player.Reborn)
            {
                case 2: Reborn.Text = "2nd Reborn"; break;
                case 1: Reborn.Text = "1st Reborn"; break;
                default: Reborn.Text = "Nono"; break;
            }
            double x = 0;
            if ((client.Player.ExpireVip > DateTime.Now))
            {
                x = (client.Player.ExpireVip - DateTime.Now).TotalDays;
            }
            textBox3.Text = x.ToString();
            textBox2.Text = client.Player.VipLevel.ToString();
        }
        private void button6_Click(object sender, EventArgs e)
        {
            Client.GameClient client = null;
            client = Client.GameClient.CharacterFromName(comboBox1.Text);
            if (client == null)
                return;
            Database.ItemType.DBItem DBItem;
            string[] id = comboBox3.Text.Split(' ').ToArray();
            if (Database.Server.ItemsBase.TryGetValue(uint.Parse(id[1]), out DBItem))
            {
                using (var rec = new ServerSockets.RecycledPacket())
                {
                    var stream = rec.GetStream();
                    //client.Inventory.Add(uint.Parse(id[1]), byte.Parse(this.Plus.Text),  DBItem, stream);
                    client.Inventory.Add(stream, uint.Parse(id[1]), byte.Parse(this.count.Text), byte.Parse(this.Plus.Text), byte.Parse(this.Bless.Text),
                        byte.Parse(this.HP.Text), (Role.Flags.Gem)byte.Parse(this.Soc1.Text), (Role.Flags.Gem)byte.Parse(this.Soc2.Text), false, (Role.Flags.ItemEffect)byte.Parse(this.Effect.Text));
                }
            }

        }
        private void button5_Click_1(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            foreach (var user in Database.Server.GamePoll.Values)
            {
                comboBox1.Items.Add(user.Player.Name);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (var user in Database.Server.GamePoll.Values)
            {
                if (user.Player.Name.ToLower() == comboBox1.Text.ToLower())
                {
                    Database.SystemBannedAccount.AddBan(user.Player.UID, user.Player.Name, uint.Parse(textBox1.Text));
                    user.Socket.Disconnect();
                    break;
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            JiangHu cp = new JiangHu();
            cp.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Chi cp = new Chi();
            cp.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Client.GameClient client = null;
            client = Client.GameClient.CharacterFromName(comboBox1.Text);
            if (client == null)
                return;


            if (!client.Player.Name.Contains("[PM]"))
            {
                Game.MsgServer.MsgNameChange.ChangeName(client, client.Player.Name + "[PM]", true);
            }
            if (!client.HelpDesk)
            {
                client.HelpDesk = true;

                client.Player.Level = 255;
                System.Windows.Forms.MessageBox.Show(client.Player.Name + " is now helpdesk");
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Client.GameClient client = null;
            client = Client.GameClient.CharacterFromName(comboBox1.Text);
            if (client == null)
                return;
            if (!client.ProjectManager)
            {
                // client.ProjectManager = true;
            }
        }

        private void button10_Click(object sender, EventArgs e)//Trojan
        {
            Client.GameClient client = null;
            client = Client.GameClient.CharacterFromName(comboBox1.Text);
            if (client == null)
                return;
            if (!client.Inventory.HaveSpace(30))
            {
                System.Windows.Forms.MessageBox.Show("Character must have 30 free slots into inventory");
                return;
            }
            using (var rec = new ServerSockets.RecycledPacket())
            {
                var stream = rec.GetStream();
                client.Inventory.Add(stream, 3004247);//P7Weponsoulpack
                client.Inventory.Add(stream, 3004247);//P7Weponsoulpack

                client.Inventory.Add(stream, 3004248);//P7EquipmentSoulpack
                client.Inventory.Add(stream, 3004248);//P7EquipmentSoulpack
                client.Inventory.Add(stream, 3004248);//P7EquipmentSoulpack
                client.Inventory.Add(stream, 3004248);//P7EquipmentSoulpack
                client.Inventory.Add(stream, 3004248);//P7EquipmentSoulpack

                client.Inventory.Add(stream, 3004249);//P6Refpack
                client.Inventory.Add(stream, 3004249);//P6Refpack
                client.Inventory.Add(stream, 3004249);//P6Refpack
                client.Inventory.Add(stream, 3004249);//P6Refpack
                client.Inventory.Add(stream, 3004249);//P6Refpack
                client.Inventory.Add(stream, 3004249);//P6Refpack
                client.Inventory.Add(stream, 3004249);//P6Refpack
            }
            using (var rec = new ServerSockets.RecycledPacket())
            {
                var stream = rec.GetStream();
                client.Inventory.AddSoul(300000, 0, 0, 0, 12, 0, 0, 0, 0, 1, stream, false);//Steed
                client.Inventory.AddSoul(203009, 0, 0, 0, 12, 0, 0, 0, 1, 1, stream, false);//Crop
                client.Inventory.AddSoul(204009, 0, 0, 0, 12, 103, 123, 0, 1, 1, stream, false);//wing
                client.Inventory.AddSoul(201009, 0, 0, 0, 12, 103, 103, 0, 1, 1, stream, false);//Fan
                client.Inventory.AddSoul(202009, 0, 0, 0, 12, 123, 123, 0, 1, 1, stream, false);//Tower
                client.Inventory.AddSoul(120269, 821033, 6, 12, 12, 13, 13, 255, 7, 1, stream, false);//Necklace
                client.Inventory.AddSoul(150269, 823059, 6, 12, 12, 13, 13, 255, 7, 1, stream, false);//Ring
                client.Inventory.AddSoul(160249, 824018, 6, 12, 12, 13, 13, 255, 7, 1, stream, false);//Boot
                client.Inventory.AddSoul(410439, 800111, 6, 12, 12, 13, 13, 255, 7, 2, stream, false);//SkyBlade
                client.Inventory.AddSoul(410439, 800111, 6, 12, 12, 13, 13, 255, 7, 2, stream, false);//SquallSword
                client.Inventory.AddSoul(480439, 800111, 6, 12, 12, 13, 13, 255, 7, 2, stream, false);//NirvanaClub
                client.Inventory.AddSoul(614439, 800111, 6, 12, 12, 13, 13, 255, 7, 2, stream, false);//FangCrossSaber
                client.Inventory.AddSoul(614439, 800111, 6, 12, 12, 13, 13, 255, 7, 2, stream, false);//FangCrossSaber
                client.Inventory.AddSoul(130309, 822071, 6, 12, 12, 13, 13, 255, 7, 1, stream, false);//ObsidianArmor
                client.Inventory.AddSoul(118309, 820075, 6, 12, 12, 13, 13, 255, 7, 1, stream, false);//PeerlessCoronet
            }
        }
        private void button11_Click(object sender, EventArgs e)
        {
            Client.GameClient client = null;
            client = Client.GameClient.CharacterFromName(comboBox1.Text);
            if (client == null)
                return;
            if (!client.Inventory.HaveSpace(30))
            {
                System.Windows.Forms.MessageBox.Show("Character must have 30 free slots into inventory");
                return;
            }
            using (var rec = new ServerSockets.RecycledPacket())
            {
                var stream = rec.GetStream();
                client.Inventory.Add(stream, 3004247);//P7Weponsoulpack
                client.Inventory.Add(stream, 3004247);//P7Weponsoulpack

                client.Inventory.Add(stream, 3004248);//P7EquipmentSoulpack
                client.Inventory.Add(stream, 3004248);//P7EquipmentSoulpack
                client.Inventory.Add(stream, 3004248);//P7EquipmentSoulpack
                client.Inventory.Add(stream, 3004248);//P7EquipmentSoulpack
                client.Inventory.Add(stream, 3004248);//P7EquipmentSoulpack

                client.Inventory.Add(stream, 3004249);//P6Refpack
                client.Inventory.Add(stream, 3004249);//P6Refpack
                client.Inventory.Add(stream, 3004249);//P6Refpack
                client.Inventory.Add(stream, 3004249);//P6Refpack
                client.Inventory.Add(stream, 3004249);//P6Refpack
                client.Inventory.Add(stream, 3004249);//P6Refpack
                client.Inventory.Add(stream, 3004249);//P6Refpack
            }
            using (var rec = new ServerSockets.RecycledPacket())
            {
                var stream = rec.GetStream();
                client.Inventory.AddSoul(300000, 0, 0, 0, 12, 0, 0, 0, 0, 1, stream, false);//Steed
                client.Inventory.AddSoul(203009, 0, 0, 0, 12, 0, 0, 0, 1, 1, stream, false);//Crop
                client.Inventory.AddSoul(204009, 0, 0, 0, 12, 103, 123, 0, 1, 1, stream, false);//wing
                client.Inventory.AddSoul(201009, 0, 0, 0, 12, 103, 103, 0, 1, 1, stream, false);//Fan
                client.Inventory.AddSoul(202009, 0, 0, 0, 12, 123, 123, 0, 1, 1, stream, false);//Tower
                client.Inventory.AddSoul(120269, 821033, 6, 12, 12, 13, 13, 255, 7, 1, stream, false);//Necklace
                client.Inventory.AddSoul(150269, 823059, 6, 12, 12, 13, 13, 255, 7, 1, stream, false);//Ring
                client.Inventory.AddSoul(160249, 824018, 6, 12, 12, 13, 13, 255, 7, 1, stream, false);//Boot
                client.Inventory.AddSoul(560439, 800215, 6, 12, 12, 13, 13, 255, 7, 1, stream, false);//SpearOfWrath
                client.Inventory.AddSoul(561439, 800215, 6, 12, 12, 13, 13, 255, 7, 1, stream, false);//OccultWand
                client.Inventory.AddSoul(624439, 800215, 6, 12, 12, 13, 13, 255, 7, 1, stream, false);//EvilSlayer
                client.Inventory.AddSoul(624439, 800215, 6, 12, 12, 13, 13, 255, 7, 1, stream, false);//EvilSlayer
                client.Inventory.AddSoul(131309, 822071, 6, 12, 12, 13, 13, 255, 7, 1, stream, false);//ImperiousArmor
                client.Inventory.AddSoul(900309, 800422, 6, 12, 12, 13, 13, 255, 7, 1, stream, false);//CelestialShield
                client.Inventory.AddSoul(111309, 820075, 6, 12, 12, 13, 13, 255, 7, 1, stream, false);//SteelHelmet
            }
        }
        private void button12_Click(object sender, EventArgs e)
        {
            Client.GameClient client = null;
            client = Client.GameClient.CharacterFromName(comboBox1.Text);
            if (client == null)
                return;
            if (!client.Inventory.HaveSpace(30))
            {
                System.Windows.Forms.MessageBox.Show("Character must have 30 free slots into inventory");
                return;
            }
            using (var rec = new ServerSockets.RecycledPacket())
            {
                var stream = rec.GetStream();
                client.Inventory.Add(stream, 3004247);//P7Weponsoulpack
                client.Inventory.Add(stream, 3004247);//P7Weponsoulpack

                client.Inventory.Add(stream, 3004248);//P7EquipmentSoulpack
                client.Inventory.Add(stream, 3004248);//P7EquipmentSoulpack
                client.Inventory.Add(stream, 3004248);//P7EquipmentSoulpack
                client.Inventory.Add(stream, 3004248);//P7EquipmentSoulpack
                client.Inventory.Add(stream, 3004248);//P7EquipmentSoulpack

                client.Inventory.Add(stream, 3004249);//P6Refpack
                client.Inventory.Add(stream, 3004249);//P6Refpack
                client.Inventory.Add(stream, 3004249);//P6Refpack
                client.Inventory.Add(stream, 3004249);//P6Refpack
                client.Inventory.Add(stream, 3004249);//P6Refpack
                client.Inventory.Add(stream, 3004249);//P6Refpack
                client.Inventory.Add(stream, 3004249);//P6Refpack
            }
            using (var rec = new ServerSockets.RecycledPacket())
            {
                var stream = rec.GetStream();

                client.Inventory.AddSoul(300000, 0, 0, 0, 12, 0, 0, 0, 0, 1, stream, false);//Steed
                client.Inventory.AddSoul(203009, 0, 0, 0, 12, 0, 0, 0, 1, 1, stream, false);//Crop
                client.Inventory.AddSoul(204009, 0, 0, 0, 12, 103, 123, 0, 1, 1, stream, false);//wing
                client.Inventory.AddSoul(201009, 0, 0, 0, 12, 103, 103, 0, 1, 1, stream, false);//Fan
                client.Inventory.AddSoul(202009, 0, 0, 0, 12, 123, 123, 0, 1, 1, stream, false);//Tower
                client.Inventory.AddSoul(120269, 821033, 6, 12, 12, 13, 13, 255, 7, 1, stream, false);//Necklace
                client.Inventory.AddSoul(150269, 823059, 6, 12, 12, 13, 13, 255, 7, 1, stream, false);//Ring
                client.Inventory.AddSoul(160249, 824018, 6, 12, 12, 13, 13, 255, 7, 1, stream, false);//Boot
                client.Inventory.AddSoul(613429, 800917, 6, 12, 12, 13, 13, 255, 7, 2, stream, false);//Knife
                client.Inventory.AddSoul(613429, 800917, 6, 12, 12, 13, 13, 255, 7, 2, stream, false);//Knife
                client.Inventory.AddSoul(500429, 800618, 6, 12, 12, 13, 13, 255, 7, 1, stream, false);//Bow
                client.Inventory.AddSoul(133309, 822071, 6, 12, 12, 13, 13, 255, 7, 1, stream, false);//ArmorArcher
                client.Inventory.AddSoul(113309, 820075, 6, 12, 12, 13, 13, 255, 7, 1, stream, false);//CapArcher
            }
        }
        private void button13_Click(object sender, EventArgs e)
        {
            Client.GameClient client = null;
            client = Client.GameClient.CharacterFromName(comboBox1.Text);
            if (client == null)
                return;
            if (!client.Inventory.HaveSpace(30))
            {
                System.Windows.Forms.MessageBox.Show("Character must have 30 free slots into inventory");
                return;
            }
            using (var rec = new ServerSockets.RecycledPacket())
            {
                var stream = rec.GetStream();
                client.Inventory.Add(stream, 3004247);//P7Weponsoulpack
                client.Inventory.Add(stream, 3004247);//P7Weponsoulpack

                client.Inventory.Add(stream, 3004248);//P7EquipmentSoulpack
                client.Inventory.Add(stream, 3004248);//P7EquipmentSoulpack
                client.Inventory.Add(stream, 3004248);//P7EquipmentSoulpack
                client.Inventory.Add(stream, 3004248);//P7EquipmentSoulpack
                client.Inventory.Add(stream, 3004248);//P7EquipmentSoulpack

                client.Inventory.Add(stream, 3004249);//P6Refpack
                client.Inventory.Add(stream, 3004249);//P6Refpack
                client.Inventory.Add(stream, 3004249);//P6Refpack
                client.Inventory.Add(stream, 3004249);//P6Refpack
                client.Inventory.Add(stream, 3004249);//P6Refpack
                client.Inventory.Add(stream, 3004249);//P6Refpack
                client.Inventory.Add(stream, 3004249);//P6Refpack
            }
            using (var rec = new ServerSockets.RecycledPacket())
            {
                var stream = rec.GetStream();
                client.Inventory.AddSoul(300000, 0, 0, 0, 12, 0, 0, 0, 0, 1, stream, false);//Steed
                client.Inventory.AddSoul(203009, 0, 0, 0, 12, 0, 0, 0, 1, 1, stream, false);//Crop
                client.Inventory.AddSoul(204009, 0, 0, 0, 12, 103, 123, 0, 1, 1, stream, false);//wing
                client.Inventory.AddSoul(201009, 0, 0, 0, 12, 103, 103, 0, 1, 1, stream, false);//Fan
                client.Inventory.AddSoul(202009, 0, 0, 0, 12, 123, 123, 0, 1, 1, stream, false);//Tower
                client.Inventory.AddSoul(120269, 821033, 6, 12, 12, 13, 13, 255, 7, 1, stream, false);//Necklace
                client.Inventory.AddSoul(150269, 823059, 6, 12, 12, 13, 13, 255, 7, 1, stream, false);//Ring
                client.Inventory.AddSoul(160249, 824018, 6, 12, 12, 13, 13, 255, 7, 1, stream, false);//Boot
                client.Inventory.AddSoul(616439, 800142, 6, 12, 12, 13, 13, 255, 7, 2, stream, false);//NinjaClaw
                client.Inventory.AddSoul(616439, 800142, 6, 12, 12, 13, 13, 255, 7, 2, stream, false);//NinjaClaw
                client.Inventory.AddSoul(511439, 800255, 6, 12, 12, 13, 13, 255, 7, 1, stream, false);//SilenceScythe
                client.Inventory.AddSoul(135309, 822071, 6, 12, 12, 13, 13, 255, 7, 1, stream, false);//NightmareVest
                client.Inventory.AddSoul(112309, 820075, 6, 12, 12, 13, 13, 255, 7, 1, stream, false);//RambleVeil
            }
        }
        private void button14_Click(object sender, EventArgs e)
        {
            Client.GameClient client = null;
            client = Client.GameClient.CharacterFromName(comboBox1.Text);
            if (client == null)
                return;
            if (!client.Inventory.HaveSpace(30))
            {
                System.Windows.Forms.MessageBox.Show("Character must have 30 free slots into inventory");
                return;
            }
            using (var rec = new ServerSockets.RecycledPacket())
            {
                var stream = rec.GetStream();
                client.Inventory.Add(stream, 3004247);//P7Weponsoulpack
                client.Inventory.Add(stream, 3004247);//P7Weponsoulpack

                client.Inventory.Add(stream, 3004248);//P7EquipmentSoulpack
                client.Inventory.Add(stream, 3004248);//P7EquipmentSoulpack
                client.Inventory.Add(stream, 3004248);//P7EquipmentSoulpack
                client.Inventory.Add(stream, 3004248);//P7EquipmentSoulpack
                client.Inventory.Add(stream, 3004248);//P7EquipmentSoulpack

                client.Inventory.Add(stream, 3004249);//P6Refpack
                client.Inventory.Add(stream, 3004249);//P6Refpack
                client.Inventory.Add(stream, 3004249);//P6Refpack
                client.Inventory.Add(stream, 3004249);//P6Refpack
                client.Inventory.Add(stream, 3004249);//P6Refpack
                client.Inventory.Add(stream, 3004249);//P6Refpack
                client.Inventory.Add(stream, 3004249);//P6Refpack
            }
            using (var rec = new ServerSockets.RecycledPacket())
            {
                var stream = rec.GetStream();
                client.Inventory.AddSoul(300000, 0, 0, 0, 12, 0, 0, 0, 0, 1, stream, false);//Steed
                client.Inventory.AddSoul(203009, 0, 0, 0, 12, 0, 0, 0, 1, 1, stream, false);//Crop
                client.Inventory.AddSoul(204009, 0, 0, 0, 12, 103, 123, 0, 1, 1, stream, false);//wing
                client.Inventory.AddSoul(201009, 0, 0, 0, 12, 103, 103, 0, 1, 1, stream, false);//Fan
                client.Inventory.AddSoul(202009, 0, 0, 0, 12, 123, 123, 0, 1, 1, stream, false);//Tower
                client.Inventory.AddSoul(120269, 821033, 6, 12, 12, 13, 13, 255, 7, 1, stream, false);//Necklace
                client.Inventory.AddSoul(150269, 823059, 6, 12, 12, 13, 13, 255, 7, 1, stream, false);//Ring
                client.Inventory.AddSoul(160249, 824018, 6, 12, 12, 13, 13, 255, 7, 1, stream, false);//Boot
                client.Inventory.AddSoul(622439, 800725, 6, 12, 12, 13, 13, 255, 7, 2, stream, false);//EpicMonk
                client.Inventory.AddSoul(622439, 800725, 6, 12, 12, 13, 13, 255, 7, 2, stream, false);//EpicMonk
                client.Inventory.AddSoul(136309, 822071, 6, 12, 12, 13, 13, 255, 7, 1, stream, false);//WhiteLotusFrock
                client.Inventory.AddSoul(143309, 820075, 6, 12, 12, 13, 13, 255, 7, 1, stream, false);//XumiCap
            }
        }
        private void button15_Click(object sender, EventArgs e)
        {
            Client.GameClient client = null;
            client = Client.GameClient.CharacterFromName(comboBox1.Text);
            if (client == null)
                return;
            if (!client.Inventory.HaveSpace(30))
            {
                System.Windows.Forms.MessageBox.Show("Character must have 30 free slots into inventory");
                return;
            }
            using (var rec = new ServerSockets.RecycledPacket())
            {
                var stream = rec.GetStream();
                client.Inventory.Add(stream, 3004247);//P7Weponsoulpack
                client.Inventory.Add(stream, 3004247);//P7Weponsoulpack

                client.Inventory.Add(stream, 3004248);//P7EquipmentSoulpack
                client.Inventory.Add(stream, 3004248);//P7EquipmentSoulpack
                client.Inventory.Add(stream, 3004248);//P7EquipmentSoulpack
                client.Inventory.Add(stream, 3004248);//P7EquipmentSoulpack
                client.Inventory.Add(stream, 3004248);//P7EquipmentSoulpack

                client.Inventory.Add(stream, 3004249);//P6Refpack
                client.Inventory.Add(stream, 3004249);//P6Refpack
                client.Inventory.Add(stream, 3004249);//P6Refpack
                client.Inventory.Add(stream, 3004249);//P6Refpack
                client.Inventory.Add(stream, 3004249);//P6Refpack
                client.Inventory.Add(stream, 3004249);//P6Refpack
                client.Inventory.Add(stream, 3004249);//P6Refpack
            }
            using (var rec = new ServerSockets.RecycledPacket())
            {
                var stream = rec.GetStream();
                client.Inventory.AddSoul(300000, 0, 0, 0, 12, 0, 0, 0, 0, 1, stream, false);//Steed
                client.Inventory.AddSoul(203009, 0, 0, 0, 12, 0, 0, 0, 1, 1, stream, false);//Crop
                client.Inventory.AddSoul(204009, 0, 0, 0, 12, 103, 123, 0, 1, 1, stream, false);//wing
                client.Inventory.AddSoul(201009, 0, 0, 0, 12, 103, 103, 0, 1, 1, stream, false);//Fan
                client.Inventory.AddSoul(202009, 0, 0, 0, 12, 123, 123, 0, 1, 1, stream, false);//Tower
                client.Inventory.AddSoul(120269, 821033, 6, 12, 12, 13, 13, 255, 7, 1, stream, false);//Necklace
                client.Inventory.AddSoul(150269, 823059, 6, 12, 12, 13, 13, 255, 7, 1, stream, false);//Ring
                client.Inventory.AddSoul(160249, 824018, 6, 12, 12, 13, 13, 255, 7, 1, stream, false);//Boot
                client.Inventory.AddSoul(611439, 800811, 6, 12, 12, 13, 13, 255, 7, 1, stream, false);//CaptainRapier
                client.Inventory.AddSoul(612439, 800810, 6, 12, 12, 13, 13, 255, 7, 1, stream, false);//LordPistol
                client.Inventory.AddSoul(139309, 822071, 6, 12, 12, 13, 13, 255, 7, 1, stream, false);//DarkDragonCoat
                client.Inventory.AddSoul(144309, 820075, 6, 12, 12, 13, 13, 255, 7, 1, stream, false);//DominatorHat
            }
        }
        private void button16_Click(object sender, EventArgs e)
        {
            Client.GameClient client = null;
            client = Client.GameClient.CharacterFromName(comboBox1.Text);
            if (client == null)
                return;
            if (!client.Inventory.HaveSpace(30))
            {
                System.Windows.Forms.MessageBox.Show("Character must have 30 free slots into inventory");
                return;
            }
            using (var rec = new ServerSockets.RecycledPacket())
            {
                var stream = rec.GetStream();
                client.Inventory.Add(stream, 3004247);//P7Weponsoulpack
                client.Inventory.Add(stream, 3004247);//P7Weponsoulpack

                client.Inventory.Add(stream, 3004248);//P7EquipmentSoulpack
                client.Inventory.Add(stream, 3004248);//P7EquipmentSoulpack
                client.Inventory.Add(stream, 3004248);//P7EquipmentSoulpack
                client.Inventory.Add(stream, 3004248);//P7EquipmentSoulpack
                client.Inventory.Add(stream, 3004248);//P7EquipmentSoulpack

                client.Inventory.Add(stream, 3004249);//P6Refpack
                client.Inventory.Add(stream, 3004249);//P6Refpack
                client.Inventory.Add(stream, 3004249);//P6Refpack
                client.Inventory.Add(stream, 3004249);//P6Refpack
                client.Inventory.Add(stream, 3004249);//P6Refpack
                client.Inventory.Add(stream, 3004249);//P6Refpack
                client.Inventory.Add(stream, 3004249);//P6Refpack
            }
            using (var rec = new ServerSockets.RecycledPacket())
            {
                var stream = rec.GetStream();
                client.Inventory.AddSoul(300000, 0, 0, 0, 12, 0, 0, 0, 0, 1, stream, false);//Steed
                client.Inventory.AddSoul(203009, 0, 0, 0, 12, 0, 0, 0, 1, 1, stream, false);//Crop
                client.Inventory.AddSoul(204009, 0, 0, 0, 12, 103, 123, 0, 1, 1, stream, false);//wing
                client.Inventory.AddSoul(201009, 0, 0, 0, 12, 103, 103, 0, 1, 1, stream, false);//Fan
                client.Inventory.AddSoul(202009, 0, 0, 0, 12, 123, 123, 0, 1, 1, stream, false);//Tower
                client.Inventory.AddSoul(120269, 821033, 6, 12, 12, 13, 13, 255, 7, 1, stream, false);//Necklace
                client.Inventory.AddSoul(150269, 823059, 6, 12, 12, 13, 13, 255, 7, 1, stream, false);//Ring
                client.Inventory.AddSoul(160249, 824018, 6, 12, 12, 13, 13, 255, 7, 1, stream, false);//Boot
                client.Inventory.AddSoul(617439, 801004, 6, 12, 12, 13, 13, 255, 7, 2, stream, false);//SkyNunchaku
                client.Inventory.AddSoul(617439, 801004, 6, 12, 12, 13, 13, 255, 7, 2, stream, false);//SkyNunchaku
                client.Inventory.AddSoul(138309, 822071, 6, 12, 12, 13, 13, 255, 7, 1, stream, false);//CombatSuit
                client.Inventory.AddSoul(148309, 820075, 6, 12, 12, 13, 13, 255, 7, 1, stream, false);//LegendHood
            }
        }
        private void button17_Click(object sender, EventArgs e)
        {
            Client.GameClient client = null;
            client = Client.GameClient.CharacterFromName(comboBox1.Text);
            if (client == null)
                return;
            if (!client.Inventory.HaveSpace(30))
            {
                System.Windows.Forms.MessageBox.Show("Character must have 30 free slots into inventory");
                return;
            }
            using (var rec = new ServerSockets.RecycledPacket())
            {
                var stream = rec.GetStream();
                client.Inventory.Add(stream, 3004247);//P7Weponsoulpack
                client.Inventory.Add(stream, 3004247);//P7Weponsoulpack

                client.Inventory.Add(stream, 3004248);//P7EquipmentSoulpack
                client.Inventory.Add(stream, 3004248);//P7EquipmentSoulpack
                client.Inventory.Add(stream, 3004248);//P7EquipmentSoulpack
                client.Inventory.Add(stream, 3004248);//P7EquipmentSoulpack
                client.Inventory.Add(stream, 3004248);//P7EquipmentSoulpack

                client.Inventory.Add(stream, 3004249);//P6Refpack
                client.Inventory.Add(stream, 3004249);//P6Refpack
                client.Inventory.Add(stream, 3004249);//P6Refpack
                client.Inventory.Add(stream, 3004249);//P6Refpack
                client.Inventory.Add(stream, 3004249);//P6Refpack
                client.Inventory.Add(stream, 3004249);//P6Refpack
                client.Inventory.Add(stream, 3004249);//P6Refpack
            }
            using (var rec = new ServerSockets.RecycledPacket())
            {
                var stream = rec.GetStream();
                client.Inventory.AddSoul(300000, 0, 0, 0, 12, 0, 0, 0, 0, 1, stream, false);//Steed
                client.Inventory.AddSoul(203009, 0, 0, 0, 12, 0, 0, 0, 1, 1, stream, false);//Crop
                client.Inventory.AddSoul(204009, 0, 0, 0, 12, 103, 123, 0, 1, 1, stream, false);//wing
                client.Inventory.AddSoul(201009, 0, 0, 0, 12, 103, 103, 0, 1, 1, stream, false);//Fan
                client.Inventory.AddSoul(202009, 0, 0, 0, 12, 123, 123, 0, 1, 1, stream, false);//Tower
                client.Inventory.AddSoul(620439, 800522, 6, 12, 12, 3, 3, 255, 7, 1, stream, false);//ImperialBacksword
                client.Inventory.AddSoul(619439, 801104, 6, 12, 12, 0, 0, 0, 0, 1, stream, false);//UniverseHossu
                client.Inventory.AddSoul(421439, 800522, 6, 12, 12, 3, 3, 255, 7, 1, stream, false);//BackSword
                client.Inventory.AddSoul(134309, 822072, 6, 12, 12, 3, 3, 255, 7, 1, stream, false);//ArmorFire
                client.Inventory.AddSoul(114309, 820074, 6, 12, 12, 3, 3, 255, 7, 1, stream, false);//CapFire
                client.Inventory.AddSoul(152279, 823060, 6, 12, 12, 3, 3, 255, 7, 1, stream, false);//Bracelet
                client.Inventory.AddSoul(121269, 821034, 6, 12, 12, 3, 3, 255, 7, 1, stream, false);//Bag
                client.Inventory.AddSoul(160249, 824018, 6, 12, 12, 3, 3, 255, 7, 1, stream, false);//Boot
            }
        }
        private void button18_Click_1(object sender, EventArgs e)
        {
            Client.GameClient client = null;
            client = Client.GameClient.CharacterFromName(comboBox1.Text);
            if (client == null)
                return;
            if (!client.Inventory.HaveSpace(30))
            {
                System.Windows.Forms.MessageBox.Show("Character must have 30 free slots into inventory");
                return;
            }
            using (var rec = new ServerSockets.RecycledPacket())
            {
                var stream = rec.GetStream();
                client.Inventory.Add(stream, 3004247);//P7Weponsoulpack
                client.Inventory.Add(stream, 3004247);//P7Weponsoulpack

                client.Inventory.Add(stream, 3004248);//P7EquipmentSoulpack
                client.Inventory.Add(stream, 3004248);//P7EquipmentSoulpack
                client.Inventory.Add(stream, 3004248);//P7EquipmentSoulpack
                client.Inventory.Add(stream, 3004248);//P7EquipmentSoulpack
                client.Inventory.Add(stream, 3004248);//P7EquipmentSoulpack

                client.Inventory.Add(stream, 3004249);//P6Refpack
                client.Inventory.Add(stream, 3004249);//P6Refpack
                client.Inventory.Add(stream, 3004249);//P6Refpack
                client.Inventory.Add(stream, 3004249);//P6Refpack
                client.Inventory.Add(stream, 3004249);//P6Refpack
                client.Inventory.Add(stream, 3004249);//P6Refpack
                client.Inventory.Add(stream, 3004249);//P6Refpack
            }
            using (var rec = new ServerSockets.RecycledPacket())
            {
                var stream = rec.GetStream();
                client.Inventory.AddSoul(120269, 821033, 6, 12, 12, 13, 13, 255, 7, 1, stream, false);//Necklace
                client.Inventory.AddSoul(150269, 823059, 6, 12, 12, 13, 13, 255, 7, 1, stream, false);//Ring
                client.Inventory.AddSoul(160249, 824018, 6, 12, 12, 13, 13, 255, 7, 1, stream, false);//Boot
                client.Inventory.AddSoul(201009, 0, 0, 0, 12, 103, 103, 0, 1, 1, stream, false);//Fan
                client.Inventory.AddSoul(202009, 0, 0, 0, 12, 123, 123, 0, 1, 1, stream, false);//Tower
                client.Inventory.AddSoul(300000, 0, 0, 0, 12, 0, 0, 0, 0, 1, stream, false);//Steed
                client.Inventory.AddSoul(203009, 0, 0, 0, 12, 0, 0, 0, 1, 1, stream, false);//Crop
                client.Inventory.AddSoul(626439, 801004, 6, 12, 12, 13, 13, 255, 7, 2, stream, false);//Wind
                client.Inventory.AddSoul(626439, 801004, 6, 12, 12, 13, 13, 255, 7, 2, stream, false);//Wind
                client.Inventory.AddSoul(101309, 822071, 6, 12, 12, 13, 13, 255, 7, 1, stream, false);//BrilliantWindrobe
                client.Inventory.AddSoul(170309, 820075, 6, 12, 12, 13, 13, 255, 7, 1, stream, false);//DivineCloudHat
                client.Inventory.AddSoul(204009, 0, 0, 0, 12, 103, 123, 0, 1, 1, stream, false);
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button20_Click(object sender, EventArgs e)
        {
            Client.GameClient client = null;
            client = Client.GameClient.CharacterFromName(comboBox1.Text);
            if (client == null)
                return;
            if (client.ProjectManager)
            {
                //client.ProjectManager = false;
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            try
            {
                Database.SystemBannedAccount.RemoveBan(comboBox2.Text);
                comboBox2.Items.Clear();
                foreach (var ban in Database.SystemBannedAccount.BannedPoll.Values)
                {
                    comboBox2.Items.Add(ban.Name);
                }
            }
            catch
            {
                comboBox2.Items.Clear();
                foreach (var ban in Database.SystemBannedAccount.BannedPoll.Values)
                {
                    comboBox2.Items.Add(ban.Name);
                }
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button21_Click(object sender, EventArgs e)
        {
            Client.GameClient client = null;
            client = Client.GameClient.CharacterFromName(comboBox1.Text);
            if (client == null)
                return;
            //client.Intrn = true;

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void button22_Click(object sender, EventArgs e)
        {
            Project_Terror_Light.Panels.AccountsForm cp = new Panels.AccountsForm();
            cp.ShowDialog();
        }
        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void sex_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

    }
}