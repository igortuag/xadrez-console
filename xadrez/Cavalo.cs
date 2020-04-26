using System;
using System.Collections.Generic;
using System.Text;
using tabuleiro;

namespace xadrez
{
    class Cavalo : Peca
    {
        public Cavalo(Cor cor, Tabuleiro tab) : base (cor, tab)
        {

        }
        public override string ToString()
        {
            return "C";
        }
        public bool podeMover(Posicao pos)
        {
            Peca p = Tabuleiro.peca(pos);
            return p == null || p.Cor != Cor;
        }
        public override bool[,] movimentosPossiveis()
        {
            bool[,] mat = new bool[Tabuleiro.Linhas, Tabuleiro.Colunas];
            Posicao pos = new Posicao(0, 0);

            pos.definirValores(Posicao.linha - 1, Posicao.coluna - 2);
            if (Tabuleiro.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }
            pos.definirValores(Posicao.linha - 2, Posicao.coluna - 1);
            if (Tabuleiro.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }
            pos.definirValores(Posicao.linha - 2, Posicao.coluna + 1);
            if (Tabuleiro.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }
            pos.definirValores(Posicao.linha - 1, Posicao.coluna + 2);
            if (Tabuleiro.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }
            pos.definirValores(Posicao.linha + 1, Posicao.coluna + 2);
            if (Tabuleiro.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }
            pos.definirValores(Posicao.linha + 2, Posicao.coluna + 1);
            if (Tabuleiro.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }
            pos.definirValores(Posicao.linha + 2, Posicao.coluna - 1);
            if (Tabuleiro.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }
            pos.definirValores(Posicao.linha + 1, Posicao.coluna - 2);
            if (Tabuleiro.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }

            return mat;
        }
    }
}
