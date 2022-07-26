using ComputerGraphics.Interfaces;
using ComputerGraphics.Objects;
using ComputerGraphics.Scene;
using ComputerGraphics.Types;
using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace Tests
{
    public class UnitTest1
    {
        #region Types
        [Theory]
        [ClassData(typeof(PointsTestData))]
        public void DistanceBetweenPointsIsRight(Point point1, Point point2, float expected)
        {
            var result = Point.Distance(point1, point2);
            Assert.Equal(expected, result);
        }

        [Theory]
        [ClassData(typeof(VectorWithPointsTestData))]
        public void GetVectorWithPointsReturnCorrectVector(Point point1, Point point2, Vector expected)
        {
            var result = Vector.GetVectorWithPoints(point1, point2);
            Assert.Equal((expected.X, expected.Y, expected.Z), (result.X, result.Y, result.Z));
        }

        [Theory]
        [InlineData(1, 2, 2, 3)]
        [InlineData(3, 4, 0, 5)]
        [InlineData(0, 0, 1, 1)]
        public void GetMagnitudeOfVectorReturnCorrectValue(int x, int y, int z, float expected)
        {
            var vector = new Vector(x, y, z);
            var result = vector.Magnitude();
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(0, 0, 0, 0, 0, 0)]
        [InlineData(3, 4, 0, 0.6, 0.8, 0)]
        [InlineData(0, 0, 1, 0, 0, 1)]
        public void GetNormalizedVectorReturnCorrectValue(int x, int y, int z, float expX, float expY, float expZ)
        {
            var vector = new Vector(x, y, z);
            var result = Vector.Normilize(vector);
            Assert.Equal((Math.Round(expX, 1), Math.Round(expY, 1), Math.Round(expZ, 1)), (Math.Round(result.X, 1), Math.Round(result.Y, 1), Math.Round(result.Z, 1)));
        }

        [Theory]
        [ClassData(typeof(VectorsCrossTestData))]
        public void GetVectorCrossingReturnCorrectVector(Vector vector1, Vector vector2, Vector expected)
        {
            var result = Vector.Cross(vector1, vector2);
            Assert.Equal((expected.X, expected.Y, expected.Z), (result.X, result.Y, result.Z));
        }

        #endregion

        #region Plane

        [Theory]
        [ClassData(typeof(IsIntersectionWithPlaneTestData))]
        public void IsIntersectionOfVectorAndPlaneReturnCorrectValue(Point start, Vector direction, bool expected)
        {
            Plane plane = new Plane(new Point(0, 0, 0), new Vector(1, 0, 0));
            var result = plane.IsIntersection(start, direction);
            Assert.Equal(expected, result);
        }

        [Theory]
        [ClassData(typeof(WhereIsIntersectionWithPlaneTestData))]
        public void WhereIsIntersectionOfVectorAndPlaneReturnPoint(Point start, Vector direction, Point expected)
        {
            Plane plane = new Plane(new Point(0, 0, 0), new Vector(1, 0, 0));
            var result = plane.WhereIntercept(start, direction);
            Assert.Equal((expected.X, expected.Y, expected.Z), (result.X, result.Y, result.Z));
        }

        #endregion

        #region Sphere

        [Theory]
        [ClassData(typeof(IsIntersectionWithSphereTestData))]
        public void IsIntersectionOfVectorAndSphereReturnCorrectValue(Point start, Vector direction, bool expected)
        {
            Sphere sphere = new Sphere(new Point(-7, 5, 2), 2);
            var result = sphere.IsIntersection(start, direction);
            Assert.Equal(expected, result);
        }

        [Theory]
        [ClassData(typeof(WhereIsIntersectionWithSphereTestData))]
        public void WhereIsIntersectionOfVectorAndSphereReturnPoint(Point start, Vector direction, Point expected)
        {
            Sphere sphere = new Sphere(new Point(-7, 5, 2), 2);
            var result = sphere.WhereIntercept(start, direction);
            Assert.Equal((Math.Round(expected.X, 2), Math.Round(expected.Y, 2), Math.Round(expected.Z, 2)), (Math.Round(result.X, 2), Math.Round(result.Y, 2), Math.Round(result.Z, 2)));
        }

        #endregion

    }

    #region TestData
    public class VectorWithPointsTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { new Point(0, 0, 0), new Point(1, 2, 2), new Vector(1, 2, 2) };
            yield return new object[] { new Point(1, 1, 1), new Point(1, 1, 1), new Vector(0, 0, 0) };
            yield return new object[] { new Point(-1, 1, -2), new Point(-1, 1, 0), new Vector(0, 0, 2) };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class PointsTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { new Point(0, 0, 0), new Point(1, 2, 2), 3 };
            yield return new object[] { new Point(1, 1, 1), new Point(1, 1, 1), 0 };
            yield return new object[] { new Point(-1, 1, -2), new Point(-1, 1, 0), 2 };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class VectorsCrossTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { new Vector(0, 0, 0), new Vector(1, 2, 2), new Vector(0, 0, 0) };
            yield return new object[] { new Vector(1, 1, 1), new Vector(1, 1, 1), new Vector(0, 0, 0) };
            yield return new object[] { new Vector(-1, 1, -2), new Vector(-1, 1, 0), new Vector(2, 2, 0) };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class IsIntersectionWithPlaneTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { new Point(0, 0, 1), new Vector(0, 0, 1), false };
            yield return new object[] { new Point(0, 0, 1), new Vector(-2, 0, 1), true };
            yield return new object[] { new Point(2, 1, 1), new Vector(1, 0, 0), false };
        }
        
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class WhereIsIntersectionWithPlaneTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { new Point(1, 1, 1), new Vector(-1, 1, 1), new Point(0, 2, 2) };
            yield return new object[] { new Point(1, -1, 1), new Vector(-1, 0, 0), new Point(0, -1, 1) };
            yield return new object[] { new Point(0, 1, 1), new Vector(-1, 1, 1), new Point(0, 1, 1) };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class IsIntersectionWithSphereTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { new Point(-0.1f, 1.8f, 1), new Vector(-0.8f, 0.5f, 0.1f), true };
            yield return new object[] { new Point(1, 0, 1), new Vector(-1, 1, 1), false };
            yield return new object[] { new Point(1, 1, -10), new Vector(0.5f, -0.5f, 0.7f), false };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class WhereIsIntersectionWithSphereTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { new Point(-0.1f, 1.8f, 1), new Vector(-0.8f, 0.5f, 0.1f), new Point(-5.04f, 4.89f, 1.62f) };
            yield return new object[] { new Point(1, 1, -10), new Vector(-0.5f, 0.25f, 0.8f), new Point(-5.67f, 4.33f, 0.67f) };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
    #endregion
}
