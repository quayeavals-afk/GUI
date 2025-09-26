using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace GUI;

public partial class Form1 : Form
{
    private List<BasicBlock> blocks = new List<BasicBlock>();
    private int nextYPosition = 20;

    public Form1()
    {
        this.AutoScroll = true;

        this.Size = new Size(1255, 600);

        Button addButton = new Button();
        addButton.Text = "Добавить новый блок";
        addButton.Location = new Point(20, 20);
        addButton.Size = new Size(1200, 60);
        addButton.Click += AddNewBlock;

        this.Controls.Add(addButton);
        nextYPosition = 100;
        AddNewBlock(null, EventArgs.Empty);
    }















    private void AddNewBlock(object sender, EventArgs e)
    {
        BasicBlock newBlock;
        newBlock = new RedBlock();

        newBlock.DeleteRequested += (s, e) => RemoveBlock((BasicBlock)s);
        newBlock.ContinuedRequested += (s,e) => ContinuedBlock((BasicBlock)s);


        newBlock.Location = new Point(20, nextYPosition);

        this.Controls.Add(newBlock);
        blocks.Add(newBlock);

        nextYPosition += newBlock.Height + 10;
    }
    private void ContinuedBlock(BasicBlock blockToRemove)
    {
        RemoveBlock(blockToRemove);
        MessageBox.Show("   Умничка!!!");
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


    protected TextBox textBox, date;
    protected Button button, button1;


    public BasicBlock()
    {
        // Настройки внешнего вида блока
        this.Size = new Size(1200, 60);
        this.BackColor = Color.LightGray;

        // Настройки текстового поля
        textBox = new TextBox();
        textBox.Location = new Point(18, 18);
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
        button.Click += (s, e) => ContinuedRequested?.Invoke(this, EventArgs.Empty);


        // Настройки кнопки ✔
        button1 = new Button();
        button1.Location = new Point(1145, 5);
        button1.Size = new Size(50, 50);
        button1.Text = "✘";
        button1.BackColor = SystemColors.Control;
        button1.BackColor = Color.Red;
        button1.Click += (s, e) => DeleteRequested?.Invoke(this, EventArgs.Empty);


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
        this.BackColor = Color.DarkRed;
    }
}