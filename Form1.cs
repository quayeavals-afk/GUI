namespace GUI;

public partial class Form1 : Form
{
    private List<BasicBlock> blocks = new List<BasicBlock>(); // список для хранения всех форм
    private int nextYPosition = 20; // Следующая позиция Y для нового блока
    public Form1()
    {
        this.Size = new Size(555, 600);

        // Создаем кнопку для добавления новых блоков
        Button addButton = new Button();
        addButton.Text = "Добавить новый блок";
        addButton.Location = new Point(20, 20);
        addButton.Size = new Size(500, 30);
        addButton.Click += AddNewBlock; // Подписываемся на клик

        this.Controls.Add(addButton);

        nextYPosition = 60; // Следующий блок будет ниже кнопки

        // Создаем несколько начальных блоков
        AddNewBlock(null, EventArgs.Empty); // Красный
        AddNewBlock(null, EventArgs.Empty); // Зеленый  
        AddNewBlock(null, EventArgs.Empty); // Синий
    }
    private void AddNewBlock(object sender, EventArgs e)
    {
        BasicBlock newBlock;
        
        // Выбираем тип блока по очереди (красный, зеленый, синий)
        int blockType = blocks.Count % 3;
        
        switch (blockType)
        {
            case 0:
                newBlock = new RedBlock();
                break;
            case 1:
                newBlock = new GreenBlock();
                break;
            case 2:
                newBlock = new BlueBlock();
                break;
            default:
                newBlock = new RedBlock();
                break;
        }

        // Устанавливаем позицию
        newBlock.Location = new Point(20, nextYPosition);
        
        // Добавляем на форму и в список
        this.Controls.Add(newBlock);
        blocks.Add(newBlock);
        
        // Увеличиваем позицию для следующего блока
        nextYPosition += newBlock.Height + 10;
        
        // Если блоков много, увеличиваем высоту формы
        if (nextYPosition > this.Height - 100)
        {
            this.Height += 100;
        }
    }
}
public class BasicBlock : UserControl
    {
        protected TextBox textBox;
        protected Button button;
        
        public BasicBlock()
        {
            this.Size = new Size(500, 60);
            this.BackColor = Color.LightGray;
            
            textBox = new TextBox();
            textBox.Location = new Point(10, 20);
            textBox.Width = 300;
            textBox.Text = "Введите текст";
            
            button = new Button();
            button.Location = new Point(410, 18);
            button.Size = new Size(80, 35);
            button.Text = "Кнопка";
            button.Click += (s, e) => MessageBox.Show("Нажата кнопка!");
            
            this.Controls.Add(textBox);
            this.Controls.Add(button);
        }
    }
 // Кубик с красной кнопкой
    public class RedBlock : BasicBlock
    {
    public RedBlock()
    {
        button.BackColor = Color.Red;
        button.Text = "Красная";
        this.BackColor = Color.LightPink; 
        

        }
    }

    // Кубик с зеленой кнопкой
    public class GreenBlock : BasicBlock
    {
        public GreenBlock()
        {
            button.BackColor = Color.Green;
            button.Text = "Зеленая";
        }
    }

    // Кубик с синей кнопкой
    public class BlueBlock : BasicBlock
    {
        public BlueBlock()
        {
            button.BackColor = Color.Blue;
            button.Text = "Синяя";
            button.ForeColor = Color.White;
        }
    }
