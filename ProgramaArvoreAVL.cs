using System;
using System.Net;

namespace ArvoreAVL
{
	class ProgramaArvoreAVL
	{
		static void Main(string[] args)
		{
			Console.WriteLine("arvore avl!");

			ArvoreAVL arvoreAVL = new ArvoreAVL();
			int opcao, x;
			
			while (true)
			{
				// exemplo
				// 70 40 20 60 80 55 85 10 15 5

				Console.WriteLine("1 - exibir a arvore");
				Console.WriteLine("2 - inserir um novo no");
				Console.WriteLine("3 - remover um no");
				Console.WriteLine("4 - percorrer em ordem");
				Console.WriteLine("5 - sair");
				Console.Write("escolha uma opcao: ");
				opcao = Convert.ToInt32(Console.ReadLine());

				if (opcao == 5)
					break;

				switch (opcao)
				{
					case 1:
						arvoreAVL.Exibir();
						break;

					case 2:
						Console.Write("entre com a chave que sera inserida: ");
						x = Convert.ToInt32(Console.ReadLine());
						arvoreAVL.Inserir(x);
						break;

					case 3:
						Console.Write("entre com a chave que sera removida: ");
						x = Convert.ToInt32(Console.ReadLine());
						arvoreAVL.Remover(x);
						break;

					case 4:
						arvoreAVL.PercorrerEmOrdem();
						break;
				}
			}
		}
	}
}
