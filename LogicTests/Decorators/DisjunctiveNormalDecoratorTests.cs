﻿using Logic;
using Logic.Decorators;
using Xunit;

namespace LogicTests.Decorators
{
    public class DisjunctiveNormalDecoratorTests
    {
        [Fact]
        public void CompleteDisjunctveDecoratorTest()
        {
            var parser = StringParser.Create("|(|(a,b), c)");


            var table = new TruthTableCreator(parser.GetOperator());


            var decorator = new DisjunctiveNormalDecorator(table);


            var processed = new TruthTableCreator(decorator.GetOperator());

            var dataOriginal = table.GetTable();
            var dataProcessed = processed.GetTable();
            for (var i = 0; i < dataOriginal.Length; i++)
            for (var j = 0; j < dataOriginal[i].Length; j++)
                Assert.False(dataOriginal[i][j] != dataProcessed[i][j]);
        }
    }
}