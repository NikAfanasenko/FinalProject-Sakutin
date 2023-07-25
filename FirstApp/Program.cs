int playerHealth = 100;
int wall = 0;
int emptyRoom = 1;
int enemy = 2;
int healthFountain = 3;
int boss = 4;
bool isAlive = true;
bool isEnd = false;


int fountainHealth = 40;
int enemyDamage = 30;
int bossDamage = 50;

int currentPositionX = 0;
int currentPositionY = 2;

int[,] playingField = { { 0,0,1,0,0 }
                       ,{ 0,1,1,1,0 }
                       ,{ 1,2,3,1,2 }
                       ,{ 0,1,1,1,0 }
                       ,{ 0,0,4,0,0 }};
DisplayTutorial();
while (true)
{
    DisplayGame();
    if (isEnd)
    {
        EndGame();
        return;
    }    
}

void DisplayTutorial()
{
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("Добро пожаловать в подземелье!\n");
    Console.WriteLine("Здесь тебе предстоит пройти лабиринт .\n");
    Console.WriteLine("Лабиринт состоит из стен, пустых комнат, комнат с врагами, а также с фантанами пополняющие здоровье .");
    Console.ResetColor();
    MiniStop();
    Console.WriteLine("Это текущий лабиринт ");
    PrintLabyrinthWithrPosition(playingField,1,1,ConsoleColor.White);
    Console.WriteLine("0 - это стены ");
    Console.WriteLine("1 - это пустые комнаты ");
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("2 - это враги ");
    Console.ForegroundColor = ConsoleColor.Blue;
    Console.WriteLine("3 - это исцеляющие фантаны ");
    Console.ForegroundColor = ConsoleColor.Magenta;
    Console.WriteLine("4 - это босс ");
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("\n При посещении не пустых комнат они пропадают!");
    Console.ForegroundColor = ConsoleColor.White;
    MiniStop();
    Console.WriteLine("Вы находитесь здесь \n");
    PrintLabyrinthWithrPosition(playingField, currentPositionX, currentPositionY, ConsoleColor.Red);
    Console.WriteLine("Босс находиться тут \n");
    PrintLabyrinthWithrPosition(playingField, 4, currentPositionY, ConsoleColor.Magenta);
    Console.WriteLine("\n Передвигаться можно по клавишам W, A, S, D ");
    MiniStop();
}
void DisplayGame()
{
    Console.Clear();    
    DisplayRoomInfo(playingField, currentPositionX, currentPositionY);
    
    Console.WriteLine($"Текущее здоровье игрока : {playerHealth}");
    Console.WriteLine("0 - это стены ");
    Console.WriteLine("1 - это пустые комнаты ");
    Console.WriteLine("2 - это враги ");
    Console.WriteLine("3 - это исцеляющие фантаны ");
    Console.WriteLine("4 - это босс \n");
    Console.WriteLine("Вы находитесь здесь \n");
    PrintLabyrinthWithrPosition(playingField, currentPositionX, currentPositionY, ConsoleColor.Red);
    if (isEnd)
    {
        return;
    }
    Console.WriteLine("\n Нажмите W, A, S, D для передвижения");
    InputKeys();
}

void EndGame()
{
    if (isAlive)
    {
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("Поздравляю ты прошел подземелье !!! ");
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("YOUR DIED ! ");
    }
    Console.ResetColor();
}

void DisappearCustomRoom(int [,] place,int positionX,int positionY)
{
    Console.WriteLine("Комната стала обычной !");
    place[positionX, positionY] = emptyRoom;
}

void DisplayRoomInfo(int[,] place, int positionX, int positionY)
{
    int roomNumber = place[positionX, positionY];
    string Discription = "";
    int ChangeHealth = 0;
    bool IsCustom = false;
    if (roomNumber == 2)
    {
        Discription = $"Вы находитесь в комнате врага, он наносит {enemyDamage} урона \n";
        ChangeHealth = -enemyDamage;
        IsCustom = true;
    }
    else if (roomNumber == 3)
    {
        Discription = $"Вы находитесь в комнате с фонтаном, он восстанавливает {fountainHealth} HP \n";
        ChangeHealth = fountainHealth;
        IsCustom = true;
    }
    else if (roomNumber == 4)
    {
        Discription = $"Вы находитесь в комнате босса, он наносит {bossDamage} урона \n";
        ChangeHealth = -bossDamage;
        IsCustom = true;
        isEnd = true;
    }
    Console.Write(Discription);
    playerHealth += ChangeHealth;
    if (playerHealth < 0)
    {
        isAlive = false;
        isEnd = true;
    }
    if (IsCustom)
    {
        DisappearCustomRoom(place, positionX, positionY);
    }    
}

void InputKeys()
{
    ConsoleKeyInfo key = Console.ReadKey();
    if (key.KeyChar == 'W' || key.KeyChar == 'w')
    {
        SetPosition(playingField, currentPositionX, currentPositionY - 1);
    }
    else if (key.KeyChar == 'A' || key.KeyChar == 'a')
    {
        SetPosition(playingField, currentPositionX - 1, currentPositionY);
    }
    else if (key.KeyChar == 'S' || key.KeyChar == 's')
    {
        SetPosition(playingField, currentPositionX, currentPositionY + 1);
    }
    else if (key.KeyChar == 'D' || key.KeyChar == 'd')
    {
        SetPosition(playingField, currentPositionX + 1, currentPositionY);
    }
}

void MiniStop()
{
    Console.WriteLine();
    Console.WriteLine("\n Нажмите любую клавишу для продолжения ...");
    Console.ReadKey();
    Console.Clear();
}

void SetPosition(int[,] place,int x, int y)
{
    if (x == -1|| y == -1 || y == 5 || place[x, y] == 0)
    {
        return;
    }
    else
    {
        currentPositionX = x;
        currentPositionY = y;
    }
}

void PrintLabyrinthWithrPosition(int[,] labyrinth, int positionX, int positionY, ConsoleColor color)
{
    for (int i = 0; i < labyrinth.GetLength(0); i++)
    {
        for (int j = 0; j < labyrinth.GetLength(1); j++)
        {
            if (i == positionY && j == positionX)
            {
                Console.ForegroundColor = color;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.White;
            }
            Console.Write(labyrinth[j, i] + " ");
        }
        Console.WriteLine();
    }
    Console.WriteLine();
}