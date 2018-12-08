using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VisionsConstructionLLC.Database.Repository.Gallery;

namespace VisionsConstructionLLC.UnitTests.Repository.Gallery {
	[TestClass]
	public class ItemRepositoryTest {
		[TestMethod]
		public void TestMethod1() {
			IItemImageRepository iItemImageRepository = new ItemImageRepository();
			Assert.IsNotNull(iItemImageRepository.findAll());
		}
	}
}