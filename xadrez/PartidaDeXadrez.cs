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

        public PartidaDeXadrez()
        {
            tab = new Tabuleiro(8, 8);
            turno = 1;
            jogadorAtual = Cor.Branca;
            terminada = false;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            colocarPecas();
        }

        public void executaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = tab.retirarPeca(origem);
            p.incrementarQteMovimento();
            Peca pecaCapturada = tab.retirarPeca(destino);
            if (pecaCapturada != null){
                capturadas.Add(pecaCapturada);
            }
            tab.colocarPeca(p, destino);
        }

        public void realizaJogada(Posicao origem, Posicao destino)
        {

            executaMovimento(origem, destino);
            turno++;
            mudaJogador();
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
            if (!tab.peca(origem).podeMoverPara(destino))
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

        public void colocarNovaPeça(char coluna, int linha, Peca peca)
        {
            tab.colocarPeca(peca, new PosicaoXadrez(coluna, linha).toPosicao());
            pecas.Add(peca);
        }

        private void colocarPecas() {
            colocarNovaPeça('c', 1, new Torre(Cor.Branca, tab));
            colocarNovaPeça('c', 2, new Torre(Cor.Branca, tab));
            colocarNovaPeça('d', 2, new Torre(Cor.Branca, tab));
            colocarNovaPeça('e', 2, new Torre(Cor.Branca, tab));
            colocarNovaPeça('e', 1, new Torre(Cor.Branca, tab));
            colocarNovaPeça('d', 1, new Rei(Cor.Branca, tab));

            colocarNovaPeça('c', 7, new Torre(Cor.Preta, tab));
            colocarNovaPeça('c', 8, new Torre(Cor.Preta, tab));
            colocarNovaPeça('d', 7, new Torre(Cor.Preta, tab));
            colocarNovaPeça('e', 7, new Torre(Cor.Preta, tab));
            colocarNovaPeça('e', 8, new Torre(Cor.Preta, tab));
            colocarNovaPeça('d', 8, new Rei(Cor.Preta, tab));
        }
    }
}
