﻿using DeltaEngine.Commands;
using DeltaEngine.Datatypes;
using DeltaEngine.Input.Mocks;
using DeltaEngine.Platforms;
using DeltaEngine.Rendering2D.Shapes;
using NUnit.Framework;

namespace DeltaEngine.Input.Tests
{
	public class MouseDragTriggerTests : TestWithMocksOrVisually
	{
		[Test]
		public void DragMouseToCreateRectangles()
		{
			var rectangle = new FilledRect(Rectangle.Unused, Color.GetRandomColor());
			//ncrunch: no coverage start
			new Command((start, end, done) =>
			{
				rectangle.DrawArea = Rectangle.FromCorners(start, end);
				if (done)
					rectangle = new FilledRect(Rectangle.Unused, Color.GetRandomColor());
			}).Add(new MouseDragTrigger());
			//ncrunch: no coverage end
		}

		[Test, CloseAfterFirstFrame]
		public void Create()
		{
			Assert.AreEqual(MouseButton.Left, new MouseDragTrigger().Button);
			Assert.AreEqual(MouseButton.Right, new MouseDragTrigger(MouseButton.Right).Button);
		}

		[Test, CloseAfterFirstFrame]
		public void CreateFromString()
		{
			Assert.AreEqual(MouseButton.Left, new MouseDragTrigger("").Button);
			Assert.AreEqual(MouseButton.Right, new MouseDragTrigger("Right").Button);
		}

		[Test]
		public void DragMouseHorizontalToCreateRectangles()
		{
			var rectangle = new FilledRect(Rectangle.Unused, Color.GetRandomColor());
			//ncrunch: no coverage start
			new Command((start, end, done) =>
			{
				rectangle.DrawArea = new Rectangle(start.X, start.Y - 0.01f, end.X - start.X, 0.02f);
				if (done)
					rectangle = new FilledRect(Rectangle.Unused, Color.GetRandomColor());
			}).Add(new MouseDragTrigger(MouseButton.Left, DragDirection.Horizontal));
			//ncrunch: no coverage end
		}

		[Test]
		public void DragMouseVerticalToCreateRectangles()
		{
			var rectangle = new FilledRect(Rectangle.Unused, Color.GetRandomColor());
			//ncrunch: no coverage start
			new Command((start, end, done) =>
			{
				rectangle.DrawArea = new Rectangle(start.X - 0.01f, start.Y, 0.02f, end.Y - start.Y);
				if (done)
					rectangle = new FilledRect(Rectangle.Unused, Color.GetRandomColor());
			}).Add(new MouseDragTrigger(MouseButton.Left, DragDirection.Vertical));
			//ncrunch: no coverage end
		}

		[Test]
		public void DragMouseIsPressingSetPosition()
		{
			var mouse = Resolve<MockMouse>();
			var trigger = new MouseDragTrigger();
			new Command(() => { }).Add(trigger);
			mouse.SetButtonState(MouseButton.Left, State.Pressing);
			AdvanceTimeAndUpdateEntities();
			Assert.AreEqual(trigger.StartPosition, mouse.Position);
			mouse.SetPosition(Vector2D.One);
			AdvanceTimeAndUpdateEntities();
			Assert.AreEqual(trigger.Position, mouse.Position);
			mouse.SetButtonState(MouseButton.Left, State.Releasing);
			AdvanceTimeAndUpdateEntities();
			Assert.IsTrue(trigger.DoneDragging);
		}

		[Test]
		public void DragMouseHorizontally()
		{
			var mouse = Resolve<MockMouse>();
			var trigger = new MouseDragTrigger(MouseButton.Left, DragDirection.Horizontal);
			new Command(() => { }).Add(trigger);
			mouse.SetPosition(new Vector2D(0.3f, 0.5f));
			mouse.SetButtonState(MouseButton.Left, State.Pressing);
			AdvanceTimeAndUpdateEntities();
			mouse.SetPosition(new Vector2D(0.7f, 0.5f));
			AdvanceTimeAndUpdateEntities();
			Assert.AreEqual(trigger.Position, mouse.Position);
			mouse.SetButtonState(MouseButton.Left, State.Releasing);
			AdvanceTimeAndUpdateEntities();
			Assert.IsTrue(trigger.DoneDragging);
		}

		[Test]
		public void DragMouseVertically()
		{
			var mouse = Resolve<MockMouse>();
			var trigger = new MouseDragTrigger(MouseButton.Left, DragDirection.Vertical);
			new Command(() => { }).Add(trigger);
			mouse.SetPosition(new Vector2D(0.3f, 0.2f));
			mouse.SetButtonState(MouseButton.Left, State.Pressing);
			AdvanceTimeAndUpdateEntities();
			mouse.SetPosition(new Vector2D(0.3f, 0.6f));
			AdvanceTimeAndUpdateEntities();
			Assert.AreEqual(trigger.Position, mouse.Position);
			mouse.SetButtonState(MouseButton.Left, State.Releasing);
			AdvanceTimeAndUpdateEntities();
			Assert.IsTrue(trigger.DoneDragging);
		}
	}
}