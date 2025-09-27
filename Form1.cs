using System;
using System.Linq; 
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace GUI;

public partial class Form1 : Form
{
    private List<BasicBlock> blocks = new List<BasicBlock>();
    private int nextYPosition = 20;
    private int count = 0;

    private TextBox mainTextBox;
    public Form1()
    {
        this.AutoScroll = true;

        this.Size = new Size(1255, 600);

        Button addButton = new Button();
        addButton.Text = "Добавить новый блок";
        addButton.Location = new Point(820, 20);
        addButton.Size = new Size(400, 60);
        addButton.Click += AddNewBlock;


        mainTextBox = new TextBox();
        mainTextBox.Location = new Point(18, 18);
        mainTextBox.Size = new Size(800, 60);
        mainTextBox.Text = "Введите текст";
        mainTextBox.Multiline = true;
        mainTextBox.WordWrap = true;


        this.Controls.Add(addButton);
        this.Controls.Add(mainTextBox);

        nextYPosition = 100;
    }



































    private void AddNewBlock(object sender, EventArgs e)
    {
        count += 1;

        BasicBlock newBlock;
        newBlock = new BasicBlock();

        newBlock.DeleteRequested += (s, e) => RemoveBlock((BasicBlock)s);
        newBlock.ContinuedRequested += (s, e) => ContinuedBlock((BasicBlock)s);


        newBlock.Location = new Point(20, nextYPosition);
        newBlock.textBox.Text = mainTextBox.Text;


        this.Controls.Add(newBlock);
        blocks.Add(newBlock);

        
        newBlock.number.Text = $"{count}";
        if (count / 10 >= 1)
        {
            newBlock.number.Width += 32;
        }
        else
        {
            newBlock.number.Width = 32;
        }

        nextYPosition += newBlock.Height + 10;
    }
    private void ContinuedBlock(BasicBlock blockToRemove)
    {
        blockToRemove.BackColor = Color.LimeGreen;
        //RemoveBlock(blockToRemove);
    }








    private void RemoveBlock(BasicBlock blockToRemove)
    {
        this.Controls.Remove(blockToRemove);
        blocks.Remove(blockToRemove);
        RearrangeBlocks();
    }
    








    private void RearrangeBlocks()
    {
        int currentY = 100;

        foreach (var block in blocks)
        {
            block.Location = new Point(20, currentY);
            currentY += block.Height + 10;
        }

        nextYPosition = currentY;
    }
}


























































// Базовый класс для всех блоков
public class BasicBlock : UserControl
{
    public event EventHandler DeleteRequested;
    public event EventHandler ContinuedRequested;


    public TextBox textBox, date, number;
    protected Button button, button1;


    public BasicBlock()
    {

        // Настройки внешнего вида блока
        this.Size = new Size(1200, 60);
        this.BackColor = Color.DarkRed;

        number = new TextBox();
        number.Location = new Point(5, 5);
        number.Font = new Font("Arial", 22);
        number.BackColor = Color.Yellow;
        number.Text = "";
        number.ReadOnly = true;
        number.Size = new Size(32, 50);

        // Настройки текстового поля
        textBox = new TextBox();
        textBox.Location = new Point(60, 5);
        textBox.Text = "";
        textBox.ReadOnly = true;
        textBox.Multiline = true;
        textBox.WordWrap = true;
        textBox.Size = new Size(700, 50);



        // Настройки текстового поля
        DateTime thisDay = DateTime.Today;

        date = new TextBox();
        date.Location = new Point(820, 20);
        date.Width = 238;
        date.Font = new Font("Arial", 11);
        date.Text = ($" дата создания: {thisDay.ToString("d")} ");
        date.BackColor = Color.Blue;


        // Настройки кнопки ✔
        button = new Button();
        button.Location = new Point(1090, 5);
        button.Size = new Size(50, 50);
        button.Text = "✔";
        button.BackColor = SystemColors.Control;
        button.BackColor = Color.Green;
        button.Click += (s, e) => ContinuedRequested?.Invoke(this, EventArgs.Empty);


        // Настройки кнопки ✘
        button1 = new Button();
        button1.Location = new Point(1145, 5);
        button1.Size = new Size(50, 50);
        button1.Text = "✘";
        button1.BackColor = SystemColors.Control;
        button1.BackColor = Color.Red;
        button1.Click += (s, e) => DeleteRequested?.Invoke(this, EventArgs.Empty);


        this.Controls.Add(textBox);
        this.Controls.Add(number);
        this.Controls.Add(date);
        this.Controls.Add(button);
        this.Controls.Add(button1);

    }
}