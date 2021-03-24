using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;

namespace ArvoreAVL
{
	class ArvoreAVL
	{
		private No raiz;
		static bool maisAlto;
		static bool maisBaixo;
		public ArvoreAVL() {
			this.raiz = null;
		}
		public bool VerificarSeVazia()
		{
			return (raiz == null);
		}

		public void Exibir()
		{
			this.Exibir(raiz, 0);
			Console.WriteLine();
		}
		private void Exibir(No no, int nivel)
		{
			int i;

			if (no == null)
				return;

			Exibir(no.noDireito, nivel + 1);
			Console.WriteLine();

			for (i = 0; i < nivel; i++)
				Console.Write("    ");

			Console.WriteLine(no.info);

			Exibir(no.noEsquerdo, nivel + 1);
		}
		// em ordem
		public void PercorrerEmOrdem()
		{
			PercorrerEmOrdem(raiz);
			Console.WriteLine();
		}
		// em ordem
		private void PercorrerEmOrdem(No no)
		{
			if (no == null)
				return;

			PercorrerEmOrdem(no.noEsquerdo);
			Console.Write(no.info + " ");
			PercorrerEmOrdem(no.noDireito);
		}

		public void Inserir(int x)
		{
			raiz = Inserir(raiz, x);
		}

		private No Inserir(No no, int x)
		{
			if (no == null)
			{
				no = new No(x);
				maisAlto = true;
			}
			else if (x < no.info)
			{
				no.noEsquerdo = Inserir(no.noEsquerdo, x);

				if (maisAlto == true)
					no = VerificarInsercaoSubarvoreEsquerda(no);

			}
			else if (x > no.info)
			{
				no.noDireito = Inserir(no.noDireito, x);

				if (maisAlto == true)
					no = VerificarInsercaoSubarvoreDireita(no);

			}
			else
			{
				Console.WriteLine(x +  " ja esta presente na arvore");
				maisAlto = false;
			}

			return no;
		}

		private No VerificarInsercaoSubarvoreEsquerda(No no)
		{
			switch (no.fatorBalanceamento) 
			{
				// caso E1, arvore balanceada
				case 0:
					no.fatorBalanceamento = 1; // agora pesada a esquerda
					break;

				// caso E2, arvore pesada a direita
				case -1:
					no.fatorBalanceamento = 0; // agora balanceada
					maisAlto = false;
					break;

				// caso E3, pesada a esquerda
				case 1:
					no = BalanceamentoEsquerdaPorInsercao(no); // balanceamento a esquerda
					maisAlto = false;
					break;
			}

			return no;
		}

		private No VerificarInsercaoSubarvoreDireita(No no)
		{
			switch (no.fatorBalanceamento) 
			{
				// caso D1, arvore balanceada
				case 0:
					no.fatorBalanceamento = -1; // agora pesada a direita
					break;

				// caso D2, arvore pesada a esquerda
				case 1:
					no.fatorBalanceamento = 0; // agora balanceada
					maisAlto = false;
					break;

				// caso D3, pesada a direita
				case -1:
					no = BalanceamentoDireitaPorInsercao(no);  // balanceamento a direita
					maisAlto = false;
					break;
			}

			return no;
		}

		private No BalanceamentoEsquerdaPorInsercao(No no)
		{
			No a, b;

			a = no.noEsquerdo;

			// caso E3A, insercao em AE
			if (a.fatorBalanceamento == 1)
			{
				no.fatorBalanceamento = 0;
				a.fatorBalanceamento = 0;
				no = RotacionarDireita(no);
			}
			else // caso E3B, insercao em AD
			{
				b = a.noDireito;

				switch (b.fatorBalanceamento)
				{
					// caso E3B1, insercao em BE
					case 1:
						no.fatorBalanceamento = -1;
						a.fatorBalanceamento = 0;
						break;
					// caso E3B2, insercao em BD
					case -1:
						no.fatorBalanceamento = 0;
						a.fatorBalanceamento = 1;
						break;
					// caso E3B3, B eh o no recentemente inserido
					case 0:
						no.fatorBalanceamento = 0;
						a.fatorBalanceamento = 0;
						break;
				}

				b.fatorBalanceamento = 0;
				no.noEsquerdo = RotacionarEsquerda(a);
				no = RotacionarDireita(no);
			}

			return no;
		}

		private No BalanceamentoDireitaPorInsercao(No no)
		{
			No a, b;

			a = no.noDireito;

			// caso D3A, insercao em AD
			if (a.fatorBalanceamento == -1)
			{
				no.fatorBalanceamento = 0;
				a.fatorBalanceamento = 0;
				no = RotacionarEsquerda(no);
			}
			else // caso D3B, insercao em AE
			{
				b = a.noEsquerdo;

				switch (b.fatorBalanceamento)
				{
					// caso D3B1, insercao em BD
					case -1:
						no.fatorBalanceamento = 1;
						a.fatorBalanceamento = 0;
						break;
					// caso D3B2, insercao em BE
					case 1:
						no.fatorBalanceamento = 0;
						a.fatorBalanceamento = -1;
						break;
					// caso E3B3, B eh o no recentemente inserido
					case 0:
						no.fatorBalanceamento = 0;
						a.fatorBalanceamento = 0;
						break;
				}

				b.fatorBalanceamento = 0;
				no.noDireito = RotacionarDireita(a);
				no = RotacionarEsquerda(no);
			}

			return no;			
		}		

		private No RotacionarDireita(No no)
		{
			No noEsquerdo = no.noEsquerdo;
			no.noEsquerdo = noEsquerdo.noDireito;
			noEsquerdo.noDireito = no;

			return noEsquerdo;
		}

		private No RotacionarEsquerda(No no)
		{
			No noDireito = no.noDireito;
			no.noDireito = noDireito.noEsquerdo;
			noDireito.noEsquerdo = no;

			return noDireito;
		}

		public void Remover(int x)
		{
			raiz = Remover(raiz, x);
		}

		private No Remover(No no, int x)
		{
			No ch, s;

			if (no == null)
			{
				Console.WriteLine(x + "nao encontrado");
				maisBaixo = false;
				
				return no;
			}
			
			if (x < no.info) // remover da subarvore a esquerda
			{
				no.noEsquerdo = Remover(no.noEsquerdo, x);
				
				if (maisBaixo == true)
					no = VerificarRemocaoSubarvoreEsquerda(no);

			} 
			else if (x > no.info) // remover da subarvore a direita
			{
				no.noDireito = Remover(no.noDireito, x);
				
				if (maisBaixo == true)
					no = VerificarRemocaoSubarvoreDireita(no);
			}
			else
			{
				// chave para remocao encontrada
				if ((no.noEsquerdo != null) && (no.noDireito != null)) // 2 filhos
				{
					s = no.noDireito;
					
					while (s.noEsquerdo != null)
						s = s.noEsquerdo;

					no.info = s.info;
					no.noDireito = Remover(no.noDireito, s.info);

					if (maisBaixo == true)
						no = VerificarRemocaoSubarvoreDireita(no);
				}
				else // 1 filho ou nenhum filho
				{	
					if (no.noEsquerdo != null) // apenas o filho esquerdo
						ch = no.noEsquerdo;
					else // apenas o filho direito ou nenhum filho
						ch = no.noDireito;

					no = ch;
					maisBaixo = true;
				}
			}

			return no;
		}

		private No VerificarRemocaoSubarvoreEsquerda(No no)
		{
			switch (no.fatorBalanceamento)
			{
				case 0: // caso E1, estava balanceado
					no.fatorBalanceamento = -1; // agora esta pesado a direita
					maisBaixo = false;
					break;
				case 1: // caso E2, estava pesado a esquerda
					no.fatorBalanceamento = 0; // agora esta balanceado
					break;
				case -1: // caso E3, estava pesado a direita
					no = BalancearDireitaPorRemocao(no); // balanceamento a direita
					break;
			}

			return no;
		}

		private No VerificarRemocaoSubarvoreDireita(No no)
		{
			switch (no.fatorBalanceamento)
			{
				case 0: // caso D1, estava balanceado
					no.fatorBalanceamento = 1; // agora esta pesado a esquerda
					maisBaixo = false;
					break;
				case -1: // caso D2, estava pesado a direita
					no.fatorBalanceamento = 0; // agora esta balanceado
					break;
				case 1: // caso D3, estava pesado a direita
					no = BalancearEsquerdaPorRemocao(no); // balanceamento a esquerda
					break;
			}

			return no;			
		}

		private No BalancearDireitaPorRemocao(No no)
		{
			No a, b;

			a = no.noDireito;

			if (a.fatorBalanceamento == 0) // caso E3A
			{
				a.fatorBalanceamento = 1;
				maisBaixo = false;
				no = RotacionarEsquerda(no);
			}
			else if (a.fatorBalanceamento == -1) // caso E3B
			{
				no.fatorBalanceamento = 0;
				a.fatorBalanceamento = 0;
				no = RotacionarEsquerda(no);
			}
			else // caso E3C
			{
				b = a.noEsquerdo;

				switch (b.fatorBalanceamento)
				{
					case 0: // caso E3C1
						no.fatorBalanceamento = 0;
						a.fatorBalanceamento = 0;
						
						break;

					case 1: // caso E3C2
						no.fatorBalanceamento = 0;
						a.fatorBalanceamento = -1;
						
						break;

					case -1: // caso E3C3
						no.fatorBalanceamento = 1;
						a.fatorBalanceamento = 0;

						break;
				}

				b.fatorBalanceamento= 0;
				no.noDireito = RotacionarDireita(a);
				no = RotacionarEsquerda(no);
			}

			return no;
		}

		private No BalancearEsquerdaPorRemocao(No no)
		{
			No a, b;

			a = no.noEsquerdo;

			if (a.fatorBalanceamento == 0) // caso D3A
			{
				a.fatorBalanceamento = -1;
				maisBaixo = false;
				no = RotacionarDireita(no);
			}
			else if (a.fatorBalanceamento == 1) // caso D3B
			{
				no.fatorBalanceamento = 0;
				a.fatorBalanceamento = 0;
				no = RotacionarDireita(no);
			}
			else // caso D3C
			{
				b = a.noDireito;

				switch (b.fatorBalanceamento)
				{
					case 0: // caso D3C1
						no.fatorBalanceamento = 0;
						a.fatorBalanceamento = 0;
						
						break;

					case 1: // caso D3C2
						no.fatorBalanceamento = 0;
						a.fatorBalanceamento = 1;
						
						break;

					case -1: // caso D3C3
						no.fatorBalanceamento = -1;
						a.fatorBalanceamento = 0;

						break;
				}

				b.fatorBalanceamento = 0;
				no.noEsquerdo = RotacionarEsquerda(a);
				no = RotacionarDireita(no);
			}

			return no;
		}		
	}
}