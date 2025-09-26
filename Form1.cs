using System;
using System.ComponentModel;
namespace GUI;

public partial class Form1 : Form
{
    private List<BasicBlock> blocks = new List<BasicBlock>();
    private int nextYPosition = 20;
    private string saveFilePath = "blocks.jsom"; // Путь к файлу сохранения


    public Form1()
    {
        this.Size = new Size(1255, 600);

        Button addButton = new Button();
        addButton.Text = "Добавить новый блок";
        addButton.Location = new Point(20, 20);
        addButton.Size = new Size(1200, 60);
        addButton.Click += AddNewBlock;

        this.Controls.Add(addButton);
        nextYPosition = 100;

        AddNewBlock(null, EventArgs.Empty);
        AddStoryBlock();
    }

    private void AddNewBlock(object sender, EventArgs e)
    {
        BasicBlock newBlock;
        newBlock = new RedBlock();

        newBlock.Location = new Point(20, nextYPosition);

        this.Controls.Add(newBlock);
        blocks.Add(newBlock);

        Console.Write($"\n какие:{blocks} \n всего: {blocks.Count}");


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


}

// Базовый класс для всех блоков
public class BasicBlock : UserControl
{
    protected TextBox textBox, date;
    protected Button button, button1;


    public BasicBlock()
    {
        // Настройки внешнего вида блока
        this.Size = new Size(1200, 60);
        this.BackColor = Color.LightGray;

        // Настройки текстового поля
        textBox = new TextBox();
        textBox.Location = new Point(10, 20);
        textBox.Width = 700;
        textBox.Text = "Введите текст";

        // Настройки текстового поля
        DateTime thisDay = DateTime.Today;

        date = new TextBox();
        date.Location = new Point(820, 20);
        date.Width = 230;
        date.Font = new Font("Arial", 11); 
        date.Text = ($"дата создания: {thisDay.ToString("d")}");
        date.BackColor = Color.Blue;


        // Настройки кнопки ✔
        button = new Button();
        button.Location = new Point(1090, 5);
        button.Size = new Size(50, 50);
        button.Text = "✔";
        button.BackColor = SystemColors.Control;
        button.BackColor = Color.Green;


        // Настройки кнопки ✔
        button1 = new Button();
        button1.Location = new Point(1145, 5);
        button1.Size = new Size(50, 50);
        button1.Text = "✘";
        button1.BackColor = SystemColors.Control;
        button1.BackColor = Color.Red;



        this.Controls.Add(textBox);
        this.Controls.Add(date);
        this.Controls.Add(button);
        this.Controls.Add(button1);

    }
}

// Красный блок
public class RedBlock : BasicBlock
{
    public RedBlock()
    {
        this.BackColor = Color.BlueViolet;
    }
}

// Блок для историй
public class StoryBlock : BasicBlock
{
    public StoryBlock()
    {
        textBox.Text = "история";
        this.BackColor = Color.DarkRed;
    }
    
}
