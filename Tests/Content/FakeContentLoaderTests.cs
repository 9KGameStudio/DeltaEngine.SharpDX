﻿using System.IO;
using DeltaEngine.Content;
using NUnit.Framework;

namespace DeltaEngine.Tests.Content
{
	public class FakeContentLoaderTests
	{
		[SetUp]
		public void CreateContentLoader()
		{
			ContentLoader.Use<FakeContentLoader>();
		}

		[TearDown]
		public void DisposeContentLoader()
		{
			ContentLoader.DisposeIfInitialized();
		}

		[Test]
		public void ContentLoadWithNullStream()
		{
			Assert.DoesNotThrow(() => ContentLoader.Load<DynamicXmlMockContent>("XmlContentWithNoPath"));
		}

		[Test]
		public void ContentLoadWithWrongFilePath()
		{
			Assert.Throws<ContentLoader.ContentFileDoesNotExist>(
				() => ContentLoader.Load<DynamicXmlMockContent>("ContentWithWrongPath"));
		}

		[Test]
		public void ThrowExceptionIfSecondContentLoaderInstanceIsUsed()
		{
			ContentLoader.Exists("abc");
			Assert.Throws
				<ContentLoader.ContentLoaderAlreadyExistsItIsOnlyAllowedToSetBeforeTheAppStarts>(
					() => ContentLoader.Use<FakeContentLoader>());
		}

		[Test]
		public void LoadDefaultDataIfAllowed()
		{
			Assert.DoesNotThrow(
				() => ContentLoader.Load<DynamicXmlMockContent>("UnavailableDynamicContent"));
		}

		private class DynamicXmlMockContent : ContentData
		{
			public DynamicXmlMockContent(string contentName)
				: base(contentName) {}

			protected override void DisposeData() {}
			protected override void LoadData(Stream fileData) {}
			protected override bool AllowCreationIfContentNotFound
			{
				get { return true; }
			}
		}
	}
}