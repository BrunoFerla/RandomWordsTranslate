using RandomTestTranslate;
using System.IO;
using System.Text;


// See https://aka.ms/new-console-template for more information


string _filePath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);

string[] lines = File.ReadAllLines($@"{_filePath}\words.csv", Encoding.ASCII);

var listWords = new List<Term>();

foreach (string line in lines)
{
    var words = line.Split(',');

    if (words.Count() > 1)
    {
        var word = words[0];
        var translates = string.Empty;

        for (int i = 1; i < words.Length; i++)
        {
            if (!string.IsNullOrEmpty(translates))
                translates += ", ";

            translates += words[i];
        }

        listWords.Add(new Term() { WordEnglish = word, WordTranslate = translates });
    }
}

var tipoIncorreto = true;
var tipo = "1";

while (tipoIncorreto)
{
    Console.WriteLine($"Como você quer brincar? palavras ordenadas ou aleatório?");
    Console.WriteLine($"[1] palavras ordenadas");
    Console.WriteLine($"[2] aleatório");
    Console.WriteLine($"[3] palavras ordenadas descendentes");
    tipo = Console.ReadLine();

    tipoIncorreto = !(tipo.Equals("1") || tipo.Equals("2") || tipo.Equals("3"));

    Console.WriteLine($"\n");

    if (tipoIncorreto)
        Console.WriteLine($"Resposta incorreta");

    Console.WriteLine($"\n");
}

var qtdWords = listWords.Count();

var qtdTentativas = 0;
var qtdeAcertos = 0;
var qtdeErros = 0;

var randomLinhaAntigo = -1;
var index = -1;
var desc = tipo == "3" ? (qtdWords - 1) : 0;

var whilee = true;

while (whilee)
{
    qtdTentativas++;

    if (tipo == "2")
    {
        while (randomLinhaAntigo == index)
        {
            index = new Random().Next(qtdWords);
        }

        randomLinhaAntigo = index;
    }
    else
    {
        index++;
    }
    
    var randomENOrPT = new Random().Next(1000);
    string resposta = string.Empty;

    var indice = Math.Abs(desc - index);


    if (randomENOrPT % 2 == 0)
    {
        Console.WriteLine($"Traduza '{listWords[indice].WordEnglish}':");
        resposta = listWords[indice].WordTranslate;
    }
    else
    {
        Console.WriteLine($"Traduza '{listWords[indice].WordTranslate}'");
        resposta = listWords[indice].WordEnglish;
    }

    resposta = resposta.Trim().ToUpper();

    var respostaUsuario = Console.ReadLine();
    respostaUsuario = respostaUsuario.Trim().ToUpper();

    if (!string.IsNullOrWhiteSpace(respostaUsuario) && resposta.Contains(respostaUsuario))
    {
        qtdeAcertos++;
        Console.WriteLine("\n");
        Console.WriteLine($"YES!!! :)  '{resposta}'");
    }
    else
    {
        qtdeErros++;

        Console.WriteLine("\n");
        Console.WriteLine($"BAD! The correct is '{resposta}'");
    }

    Console.WriteLine("\n\n");
    Console.WriteLine($"Total de tentativas {qtdTentativas} / Total de acertos {qtdeAcertos} / Total de erros {qtdeErros}");
    Console.WriteLine("------------------------------------------------------------------------------------------------------------");
    Console.WriteLine("\n");

    if (tipo != "2")
        whilee = index < (listWords.Count() -1);
    
}