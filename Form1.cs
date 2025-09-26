namespace GUI;

public partial class Form1 : Form
{
    private List<BasicBlock> blocks = new List<BasicBlock>();
    private int nextYPosition = 20;

    public Form1()
    {
        this.Size = new Size(555, 600);

        Button addButton = new Button();
        addButton.Text = "Добавить новый блок";
        addButton.Location = new Point(20, 20);
        addButton.Size = new Size(500, 30);
        addButton.Click += AddNewBlock;

        this.Controls.Add(addButton);
        nextYPosition = 60;

        AddNewBlock(null, EventArgs.Empty);
        AddStoryBlock();
        AddpopaBlock();
        AddStory1Block();
    }

    private void AddNewBlock(object sender, EventArgs e)
    {
        BasicBlock newBlock;
        newBlock = new RedBlock();

        newBlock.Location = new Point(20, nextYPosition);

        this.Controls.Add(newBlock);
        blocks.Add(newBlock);

        nextYPosition += newBlock.Height + 10;

        if (nextYPosition > this.Height - 100)
        {
            this.Height += 100;
        }
    }

    private void AddStoryBlock()
    {
        StoryBlock storyBlock = new StoryBlock();
        storyBlock.Location = new Point(20, nextYPosition);

        this.Controls.Add(storyBlock);
        blocks.Add(storyBlock);

        nextYPosition += storyBlock.Height + 10;

        if (nextYPosition > this.Height - 100)
        {
            this.Height += 100;
        }
    }
    private void AddpopaBlock()
    {
        popaBlock PopaBlock = new popaBlock();
        PopaBlock.Location = new Point(20, nextYPosition);
        this.Controls.Add(PopaBlock);
        blocks.Add(PopaBlock);

        nextYPosition += PopaBlock.Height + 10;

        if (nextYPosition > this.Height - 100)
        {
            this.Height += 100;
        }
    }
    private void AddStory1Block()
    {
        Story1Block story1Block = new Story1Block();
        story1Block.Location = new Point(20, nextYPosition);
        this.Controls.Add(story1Block);
        blocks.Add(story1Block);

        nextYPosition += story1Block.Height + 10;
        if (nextYPosition > this.Height - 100)
        {
            this.Height += 100;
        }
    }
}

// Базовый класс для всех блоков
public class BasicBlock : UserControl
{
    protected TextBox textBox;
    protected Button button;
    
    public BasicBlock()
    {
        // Настройки внешнего вида блока
        this.Size = new Size(500, 60);
        this.BackColor = Color.LightGray;
        
        // Настройки текстового поля
        textBox = new TextBox();
        textBox.Location = new Point(10, 20);
        textBox.Width = 300;
        textBox.Text = "Введите текст";
        
        // Настройки кнопки
        button = new Button();
        button.Location = new Point(410, 18);
        button.Size = new Size(80, 35);
        button.Text = "Кнопка";
        button.BackColor = SystemColors.Control;
        
        this.Controls.Add(textBox);
        this.Controls.Add(button);
    }
}

// Красный блок
public class RedBlock : BasicBlock
{
    public RedBlock()
    {
        button.BackColor = Color.Red;
        button.Text = "Красная";
        this.BackColor = Color.LightPink;
    }
}

// Блок для историй
public class StoryBlock : BasicBlock
{
    public StoryBlock()
    {
        textBox.Text = "расскажи историю";
        button.BackColor = Color.Purple;
        button.Text = "Рассказать";
        this.BackColor = Color.Lavender;
    }
}
public class popaBlock : BasicBlock
{
    public popaBlock()
    {
        textBox.Text = "соси";
        button.BackColor = Color.Green;
        button.Text = "сосать";
        this.BackColor = Color.Red;
    }
}
public class Story1Block : BasicBlock
{
    public Story1Block()
    {
        textBox.Text = "мой попу";
        button.BackColor = Color.AliceBlue;
        button.Text = "мыть";
        this.BackColor = Color.Red;
    }
}