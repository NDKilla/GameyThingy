using CoreLibrary;

namespace GUI;

public partial class MainForm : Form
{
    Thread? gameThread = null;
    public MainForm()
    {
        InitializeComponent();
        FormClosing += MainForm_FormClosing;
        Shown += MainForm_Shown;
        GameController.Ref.GameTick += Ref_GameTick;
        GameController.Ref.EventMessage += Ref_EventMessage;
    }

    private void Ref_EventMessage(object? sender, string e)
    {
        Task.Run(() =>
        {
            BeginInvoke(new Action(() =>
            {
                richTextBox1.AppendText($"{e}{Environment.NewLine}");
                richTextBox1.ScrollToCaret();
            }));
        });
    }

    private void MainForm_FormClosing(object? sender, FormClosingEventArgs e)
    {
        GameController.Ref.GameTick -= Ref_GameTick;
        GameController.Ref.EventMessage -= Ref_EventMessage;

        GameController.Ref.Exiting = true;
    }

    private void Ref_GameTick(object? sender, double e)
    {
        GameController copy = GameController.Ref;

        Task.Run(() =>
        {
            BeginInvoke(new Action(() =>
            {
                textBox1.Text = copy.Player.HP.ToString();
                textBox2.Text = copy.Player.MP.ToString();
                textBox3.Text = copy.Player.Level.ToString();
                textBox4.Text = copy.Player.XP.ToString();
                textBox5.Text = copy.Player.Strength.ToString();
                textBox6.Text = copy.Player.Dexterity.ToString();
                textBox7.Text = copy.Player.Intelligence.ToString();
                textBox8.Text = copy.Player.CritChance.ToString();
                textBox9.Text = copy.Player.CritMultiplier.ToString();
                textBox10.Text = copy.Player.AttackSpeed.ToString();
                textBox21.Text = copy.Player.SkillPoints.ToString();


                textBox20.Text = copy.Monster.HP.ToString();
                textBox19.Text = copy.Monster.MP.ToString();
                textBox18.Text = copy.Monster.Level.ToString();
                textBox17.Text = copy.Monster.XP.ToString();
                textBox16.Text = copy.Monster.Strength.ToString();
                textBox15.Text = copy.Monster.Dexterity.ToString();
                textBox14.Text = copy.Monster.Intelligence.ToString();
                textBox13.Text = copy.Monster.CritChance.ToString();
                textBox12.Text = copy.Monster.CritMultiplier.ToString();
                textBox11.Text = copy.Monster.AttackSpeed.ToString();
            }));
        });

    }

    private void MainForm_Shown(object? sender, EventArgs e)
    {

        gameThread = new Thread(GameController.Ref.MainLoop);
        gameThread.Start();
        button1.Click += (o, e) => { GameController.Ref.Player.LevelHP(); };
        button2.Click += (o, e) => { GameController.Ref.Player.LevelMP(); };
        button3.Click += (o, e) => 
        {
            GameController.Ref.Player.Level++;
            GameController.Ref.Player.SkillPoints++;
            GameController.Ref.Player.XP = 0;
        };
        button4.Click += (o, e) =>
        {
            GameController.Ref.Player.XP++;
        };
        button5.Click += (o, e) => { GameController.Ref.Player.LevelStrength(); };
        button6.Click += (o, e) => { GameController.Ref.Player.LevelDexterity(); };
        button7.Click += (o, e) => { GameController.Ref.Player.LevelIntelligence(); };
        button8.Click += (o, e) => { GameController.Ref.Player.LevelCritChance(); };
        button9.Click += (o, e) => { GameController.Ref.Player.LevelCritMultiplier(); };
        button10.Click += (o, e) => { GameController.Ref.Player.LevelAttackSpeed(); };

    }
}
