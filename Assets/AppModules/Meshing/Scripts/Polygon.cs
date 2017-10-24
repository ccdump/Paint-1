﻿using UnityEngine;

namespace Leap.Unity.Meshing {

  public struct Polygon {

    #region Vertices

    private int[] _verts;
    public int[] verts {
      get { return _verts; }
      set {
        if (value.Length < 3) {
          throw new System.InvalidOperationException(
            "Polygons must have at least 3 vertices.");
        }

        _verts = value;
      }
    }

    public int this[int idx] {
      get { return verts[idx]; }
      set { verts[idx] = value; }
    }

    public int Length { get { return _verts.Length; } }

    #endregion

    #region Operations

    /// <summary>
    /// Adds the argument amount to each vertex index in this polygon definition, and
    /// also returns this polygon for convenience.
    /// </summary>
    public Polygon IncrementIndices(int byAmount) {
      for (int i = 0; i < _verts.Length; i++) {
        _verts[i] += byAmount;
      }

      return this;
    }

    #endregion

    #region Triangulation

    public TriangleEnumerator tris {
      get { return new TriangleEnumerator(this); }
    }

    public struct TriangleEnumerator {

      private Polygon _polygon;
      private int _curIdx;

      public TriangleEnumerator(Polygon polygon) {
        _polygon = polygon;

        _curIdx = -1;
      }

      public Triangle Current {
        get {
          return new Triangle() {
            a = _polygon[0],
            b = _polygon[_curIdx + 1],
            c = _polygon[_curIdx + 2]
          };
        }
      }

      public bool MoveNext() {
        _curIdx += 1;
        return _curIdx + 2 < _polygon.Length;
      }

      public TriangleEnumerator GetEnumerator() { return this; }

    }

    #endregion

  }

}