using System;
using System.Linq; 
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Text.Json; 
using System.IO; 

namespace GUI;

public partial class Form1 : Form
{
    private List<BasicBlock> blocks = new List<BasicBlock>();
    private int nextYPosition = 20;
    private int count = 0;
    private string blocks_JSON = "blocks.json";


    private TextBox mainTextBox;
    public Form1()
    {
        this.FormClosing += Form1_FormClosing;
        this.Load += Form1_Load;

        this.AutoScroll = true;

        this.Size = new Size(1260, 900);

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
    













private void Form1_Load(object sender, EventArgs e)
    {
        LoadBlocks();
    }

private void Form1_FormClosing(object sender, FormClosingEventArgs e)
    {
        SaveBlocks();
    }








    // КЛАСС ДЛЯ СОХРАНЕНИЯ БЛОКА
    private class BlockSaveData
    {
        public string Text { get; set; }  
        public string Date {get; set;}
        public string Number { get; set; } 
        public string BackgroundColor { get; set; } 
        public int PositionY { get; set; }          
    }




    private void SaveBlocks()
    {
        Console.Write($"1 count {count}");
        List<BlockSaveData> blocksToSave = new List<BlockSaveData>();
        

        foreach (BasicBlock block in blocks)
        {
            BlockSaveData blockData = new BlockSaveData
            {
                Text = block.textBox.Text,
                Date = block.date.Text,
                Number = block.number.Text,
                BackgroundColor = block.BackColor.Name,
                PositionY = block.Location.Y
            };
            blocksToSave.Add(blockData);
        }
        Console.Write($"\n pop {blocksToSave.Count}");
        
        if (blocksToSave.Count == 0) { count = 0; }
        BlockSaveData counterData = new BlockSaveData
        {
            Number = count.ToString()
        };
        blocksToSave.Insert(0, counterData);

        JsonSerializerOptions options = new JsonSerializerOptions
        {
            WriteIndented = true
        };
        string jsonString = JsonSerializer.Serialize(blocksToSave, options);
        File.WriteAllText(blocks_JSON, jsonString);
    }






    // МЕТОД ЗАГРУЗКИ БЛОКОВ
    private void LoadBlocks()
    {
        string jsonString = File.ReadAllText(blocks_JSON);
        List<BlockSaveData> loadedBlocks = JsonSerializer.Deserialize<List<BlockSaveData>>(jsonString);

        if (loadedBlocks == null || loadedBlocks.Count == 0){return;}
        if (int.TryParse(loadedBlocks[0].Number, out int savedCount)) { count = savedCount; }

        for (int i = 1; i < loadedBlocks.Count; i++)
        {
            BlockSaveData blockData = loadedBlocks[i];
            CreateBlockFromSavedData(blockData);
        }
    }


    private void CreateBlockFromSavedData(BlockSaveData blockData)
    {
        BasicBlock newBlock = new BasicBlock();

        newBlock.textBox.Text = blockData.Text;
        newBlock.date.Text = blockData.Date;
        newBlock.number.Text = blockData.Number;

        Color color = Color.FromName(blockData.BackgroundColor);
        newBlock.BackColor = color;

        int numberValue;
        if (int.TryParse(blockData.Number, out numberValue))
        {
            if (numberValue / 10 >= 1)
            {
                newBlock.number.Width += 32;
            }
            else
            {
                newBlock.number.Width = 32;
            }
        }
        
        newBlock.DeleteRequested += (s, e) => RemoveBlock((BasicBlock)s);
        newBlock.ContinuedRequested += (s, e) => ContinuedBlock((BasicBlock)s);

        newBlock.Location = new Point(20, blockData.PositionY);
        this.Controls.Add(newBlock);
        blocks.Add(newBlock);

        nextYPosition = Math.Max(nextYPosition, blockData.PositionY + newBlock.Height + 10);
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
        //RemoveBlock(blockToRemove); //на всякий
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