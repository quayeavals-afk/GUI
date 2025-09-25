namespace MyFirstWindowsFormsApp;

public partial class Form1 : Form
{
    // Объявляем элементы управления как поля класса
    private Button myButton;
    private TextBox myTextBox;

    public Form1()
    {
        // === Создание и настройка текстового поля ===
        myTextBox = new TextBox();
        myTextBox.Location = new System.Drawing.Point(50, 50); // Позиция (X, Y)
        myTextBox.Width = 200; // Ширина
        myTextBox.Text = "Hello, World!"; // Текст по умолчанию

        // === Создание и настройка кнопки ===
        myButton = new Button();
        myButton.Location = new System.Drawing.Point(50, 80); // Позиция под текстовым полем
        myButton.Text = "Нажми меня!"; // Текст на кнопке

        // === Обработчик события нажатия на кнопку ===
        myButton.Click += MyButton_Click; // Подписываемся на событие Click

        // === Добавление элементов на форму ===
        this.Controls.Add(myTextBox); // this - это наша форма (Form1)
        this.Controls.Add(myButton);

        // === Настройка самой формы ===
        this.Text = "Мое первое приложение на WinForms"; // Заголовок окна
        this.Width = 400; // Ширина окна
        this.Height = 250; // Высота окна
    }
    // Обработчик события нажатия кнопки
    private void MyButton_Click(object sender, EventArgs e)
    {
        // Эта строка выполнится при нажатии на кнопку
        MessageBox.Show($"Ты ввел: {myTextBox.Text}");
    }
}

