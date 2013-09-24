﻿using System;
using System.Collections.Generic;
using DeltaEngine.Content;
using DeltaEngine.Core;
using DeltaEngine.Datatypes;
using DeltaEngine.Entities;
using DeltaEngine.Graphics;
using DeltaEngine.Graphics.Vertices;
using DeltaEngine.ScreenSpaces;

namespace DeltaEngine.Rendering2D.Shapes
{
	/// <summary>
	/// Responsible for rendering filled 2D shapes defined by their border points
	/// </summary>
	public class DrawPolygon2D : DrawBehavior
	{
		public DrawPolygon2D(Drawing draw, Window window)
		{
			this.draw = draw;
			this.window = window;
			material = new Material(Shader.Position2DColor, "");
		}

		private readonly Drawing draw;
		private readonly Window window;
		private readonly Material material;

		public void Draw(IEnumerable<DrawableEntity> entities)
		{
			window.Title = "DrawPolygon2D Fps: " + GlobalTime.Current.Fps;
			foreach (var entity in entities)
				AddToBatch((Entity2D)entity);
			DrawBatch();
		}

		private void AddToBatch(Entity2D entity)
		{
			var points = entity.Get<List<Vector2D>>();
			if (points.Count < 3)
				return;
			if (points.Count > CircularBuffer.TotalMaximumVerticesLimit)
				throw new TooManyVerticesForPolygon(points.Count);
			var color = entity.Color;
			if (offset + points.Count > vertices.Length)
				ResizeVertices();
			for (int num = 0; num < points.Count; num++)
				vertices[offset + num] =
					new VertexPosition2DColor(ScreenSpace.Current.ToPixelSpace(points[num]), color);
			BuildIndices(points.Count);
			offset += points.Count;
		}

		private class TooManyVerticesForPolygon : Exception
		{
			public TooManyVerticesForPolygon(int numberOfPoints)
				: base(
					"Points: " + numberOfPoints + ", Maximum: " + CircularBuffer.TotalMaximumVerticesLimit) {}
		}

		private int offset;
		private VertexPosition2DColor[] vertices = new VertexPosition2DColor[InitialVertices];
		private const int InitialVertices = 4096;

		private void ResizeVertices()
		{
			if (offset > 0)
				DrawBatch();
			if (vertices.Length >= CircularBuffer.TotalMaximumVerticesLimit)
				return;
			vertices = new VertexPosition2DColor[vertices.Length * 2];
			indices = new short[vertices.Length * 3];
		}

		private short[] indices = new short[InitialVertices * 3];

		private void DrawBatch()
		{
			draw.Add(material, vertices, indices, offset, numberOfIndicesUsed);
			offset = 0;
			numberOfIndicesUsed = 0;
		}

		private int numberOfIndicesUsed;

		private void BuildIndices(int numberOfPoints)
		{
			for (int num = 0; num < numberOfPoints - 2; num++)
			{
				indices[numberOfIndicesUsed++] = (short)offset;
				indices[numberOfIndicesUsed++] = (short)(offset + num + 1);
				indices[numberOfIndicesUsed++] = (short)(offset + num + 2);
			}
		}
	}
}