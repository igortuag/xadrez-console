using System;
using tabuleiro;
using System.Collections.Generic;

namespace xadrez
{
    class PartidaDeXadrez
    {
        public Tabuleiro tab { get; private set; }
        public int turno { get; private set; }
        public Cor jogadorAtual { get; private set; }
        public bool terminada { get; private set; }
        private HashSet<Peca> pecas;
        private HashSet<Peca> capturadas;
        public bool xeque { get; private set; }

        public PartidaDeXadrez()
        {
            tab = new Tabuleiro(8, 8);
            turno = 1;
            jogadorAtual = Cor.Branca;
            terminada = false;
            xeque = false;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            colocarPecas();
        }

        public Peca executaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = tab.retirarPeca(origem);
            p.incrementarQteMovimento();
            Peca pecaCapturada = tab.retirarPeca(destino);
            if (pecaCapturada != null)
            {
                capturadas.Add(pecaCapturada);
            }
            tab.colocarPeca(p, destino);

            // #Jogada especial roque pequeno
            if(p is Rei && destino.coluna == origem.coluna + 2)
            {
                Posicao origemT = new Posicao(origem.linha, origem.coluna + 3);
                Posicao destinoT = new Posicao(origem.linha, origem.coluna + 1);
                Peca T = tab.retirarPeca(origemT);
                T.incrementarQteMovimento();
                tab.colocarPeca(T, destinoT);
            }

            // #Jogada especial roque grande
            if (p is Rei && destino.coluna == origem.coluna - 2)
            {
                Posicao origemT = new Posicao(origem.linha, origem.coluna - 4);
                Posicao destinoT = new Posicao(origem.linha, origem.coluna - 1);
                Peca T = tab.retirarPeca(origemT);
                T.incrementarQteMovimento();
                tab.colocarPeca(T, destinoT);
            }

            return pecaCapturada;
        }

        public void realizaJogada(Posicao origem, Posicao destino)
        {

            Peca pecaCapturada = executaMovimento(origem, destino);

            if (estaEmXeque(jogadorAtual))
            {
                desfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Você não pode se colocar em xeque!");
            }

            if (estaEmXeque(adversaria(jogadorAtual)))
            {
                xeque = true;
            }
            else
            {
                xeque = false;
            }
            if (testeXequemate(adversaria(jogadorAtual)))
            {
                terminada = true;
            }
            else
            {
                turno++;
                mudaJogador();
            }
        }

        private void desfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca p = tab.retirarPeca(destino);
            p.decrementarQteMovimento();
            if (pecaCapturada != null)
            {
                tab.colocarPeca(pecaCapturada, destino);
                capturadas.Remove(pecaCapturada);
            }
            tab.colocarPeca(p, origem);

            // #Jogada especial roque pequeno
            if (p is Rei && destino.coluna == origem.coluna + 2)
            {
                Posicao origemT = new Posicao(origem.linha, origem.coluna + 3);
                Posicao destinoT = new Posicao(origem.linha, origem.coluna + 1);
                Peca T = tab.retirarPeca(destinoT);
                T.decrementarQteMovimento();
                tab.colocarPeca(T, origemT);
            }

            // #Jogada especial roque grande
            if (p is Rei && destino.coluna == origem.coluna - 2)
            {
                Posicao origemT = new Posicao(origem.linha, origem.coluna - 4);
                Posicao destinoT = new Posicao(origem.linha, origem.coluna - 1);
                Peca T = tab.retirarPeca(destinoT);
                T.decrementarQteMovimento();
                tab.colocarPeca(T, origemT);
            }
        }

        public void validarPosicaoDeOrigem(Posicao posicao)
        {
            if (tab.peca(posicao) == null)
            {
                throw new TabuleiroException("Não existe peça na posição de origem escolhida!");
            }
            if (jogadorAtual != tab.peca(posicao).Cor)
            {
                throw new TabuleiroException("A peça escolhida não pertence ao jogador do turno atual!");
            }
            if (!tab.peca(posicao).existeMovimentosPossiveis())
            {
                throw new TabuleiroException("Não há movimentos possíveis para peça escolhida.");
            }
        }

        public void validarPosicaoDeDestino(Posicao origem, Posicao destino)
        {
            if (!tab.peca(origem).movimentoPossivel(destino))
            {
                throw new TabuleiroException("Posição de destino inválida!");
            }
        }

        private void mudaJogador()
        {
            if (jogadorAtual == Cor.Branca)
            {
                jogadorAtual = Cor.Preta;
            }
            else
            {
                jogadorAtual = Cor.Branca;
            }
        }

        public HashSet<Peca> pecasCapturadas(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in capturadas)
            {
                if (x.Cor == cor)
                {
                    aux.Add(x);
                }
            }
            return aux;
        }

        public HashSet<Peca> pecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in pecas)
            {
                if (x.Cor == cor)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(pecasCapturadas(cor));
            return aux;
        }

        private Cor adversaria(Cor cor)
        {
            if (cor == Cor.Branca)
            {
                return Cor.Preta;
            }
            else
            {
                return Cor.Branca;
            }
        }

        private Peca rei(Cor cor)
        {
            foreach (Peca x in pecasEmJogo(cor))
            {
                if (x is Rei)
                {
                    return x;
                }
            }
            return null;
        }

        public bool estaEmXeque(Cor cor)
        {
            Peca R = rei(cor);
            if (R == null)
            {
                throw new TabuleiroException($"Não tem rei da cor no {cor} tabuleiro");
            }
            foreach (Peca x in pecasEmJogo(adversaria(cor)))
            {
                bool[,] mat = x.movimentosPossiveis();
                if (mat[R.Posicao.linha, R.Posicao.coluna])
                {
                    return true;
                }
            }
            return false;
        }

        public bool testeXequemate(Cor cor)
        {
            if (!estaEmXeque(cor))
            {
                return false;
            }
            foreach (Peca x in pecasEmJogo(cor))
            {
                bool[,] mat = x.movimentosPossiveis();
                for (int i = 0; i < tab.linhas; i++)
                {
                    for (int j = 0; j < tab.colunas; j++)
                    {
                        if (mat[i, j])
                        {
                            Posicao origem = x.Posicao;
                            Posicao destino = new Posicao(i, j);
                            Peca pecaCapturada = executaMovimento(origem, destino);
                            bool testeXeque = estaEmXeque(cor);
                            desfazMovimento(origem, destino, pecaCapturada);
                            if (!testeXeque)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        public void colocarNovaPeça(char coluna, int linha, Peca peca)
        {
            tab.colocarPeca(peca, new PosicaoXadrez(coluna, linha).toPosicao());
            pecas.Add(peca);
        }

        private void colocarPecas()
        {
            colocarNovaPeça('a', 1, new Torre(Cor.Branca, tab));
            colocarNovaPeça('b', 1, new Cavalo(Cor.Branca, tab));
            colocarNovaPeça('c', 1, new Bispo(Cor.Branca, tab));
            colocarNovaPeça('d', 1, new Dama(Cor.Branca, tab));
            colocarNovaPeça('e', 1, new Rei(Cor.Branca, tab, this));
            colocarNovaPeça('f', 1, new Bispo(Cor.Branca, tab));
            colocarNovaPeça('g', 1, new Cavalo(Cor.Branca, tab));
            colocarNovaPeça('h', 1, new Torre(Cor.Branca, tab));
            colocarNovaPeça('a', 2, new Peao(Cor.Branca, tab));
            colocarNovaPeça('b', 2, new Peao(Cor.Branca, tab));
            colocarNovaPeça('c', 2, new Peao(Cor.Branca, tab));
            colocarNovaPeça('d', 2, new Peao(Cor.Branca, tab));
            colocarNovaPeça('e', 2, new Peao(Cor.Branca, tab));
            colocarNovaPeça('f', 2, new Peao(Cor.Branca, tab));
            colocarNovaPeça('g', 2, new Peao(Cor.Branca, tab));
            colocarNovaPeça('h', 2, new Peao(Cor.Branca, tab));

            colocarNovaPeça('a', 8, new Torre(Cor.Preta, tab));
            colocarNovaPeça('b', 8, new Cavalo(Cor.Preta, tab));
            colocarNovaPeça('c', 8, new Bispo(Cor.Preta, tab));
            colocarNovaPeça('d', 8, new Dama(Cor.Preta, tab));
            colocarNovaPeça('e', 8, new Rei(Cor.Preta, tab, this));
            colocarNovaPeça('f', 8, new Bispo(Cor.Preta, tab));
            colocarNovaPeça('g', 8, new Cavalo(Cor.Preta, tab));
            colocarNovaPeça('h', 8, new Torre(Cor.Preta, tab));
            colocarNovaPeça('a', 7, new Peao(Cor.Preta, tab));
            colocarNovaPeça('b', 7, new Peao(Cor.Preta, tab));
            colocarNovaPeça('c', 7, new Peao(Cor.Preta, tab));
            colocarNovaPeça('d', 7, new Peao(Cor.Preta, tab));
            colocarNovaPeça('e', 7, new Peao(Cor.Preta, tab));
            colocarNovaPeça('f', 7, new Peao(Cor.Preta, tab));
            colocarNovaPeça('g', 7, new Peao(Cor.Preta, tab));
            colocarNovaPeça('h', 7, new Peao(Cor.Preta, tab));
        }
    }
}
