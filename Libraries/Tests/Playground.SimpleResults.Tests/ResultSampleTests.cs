namespace Playground.SimpleResults.Tests;

[Trait("Description", "ResultSampleTests")]
public class ResultSampleTests
{
    [Fact]
    public void CreateOrder_IncorrectUser()
    {
        // Assign
        var sut = new ResultSample();
        // Act
        var result = sut.CreateOrder(-1, [
            1,
            2,
            3,
            4
        ]);

        // Assert
        Assert.True(result.IsFailure);
        Assert.True(result.Message.Equals(Messages.User_DoesNot_Exist));
    }

    [Fact]
    public void CreateOrder_ProductNotSelected()
    {
        // Assign
        var sut = new ResultSample();
        // Act
        var result = sut.CreateOrder(2, []);

        // Assert
        Assert.True(result.IsFailure);
        Assert.True(result.Message.Equals(Messages.Products_AreEmpty));
    }


    [Fact]
    public void CreateOrder_Correct()
    {
        // Assign
        var sut = new ResultSample();
        // Act
        var result = sut.CreateOrder(2, [
            1,
            2,
            3,
            4
        ]);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.True(string.IsNullOrWhiteSpace(result.Message));
        Assert.IsType<Result<Guid>>(result);
        Assert.True((result as Result<Guid>)?.Value != null);
    }

    [Fact]
    public void GetOrder_BadRequest()
    {
        // Assign
        var sut = new ResultSample();
        // Act
        var result = sut.GetOrder(Guid.Empty);

        // Assert
        Assert.True(result.IsFailure);
        Assert.True(result.Code == 400);
        Assert.True(result.Message.Equals(Messages.OrderId_NotValid));
    }

    [Fact]
    public void GetOrder_NotFound()
    {
        // Assign
        var sut = new ResultSample();
        // Act
        var result = sut.GetOrder(Guid.NewGuid());

        // Assert
        Assert.True(result.IsFailure);
        Assert.True(result.Code == 404);
        Assert.True(result.Message.Equals(Messages.Order_DoesNotExist));
    }


    [Fact]
    public void GetOrder_Found_Okay()
    {
        // Assign
        var sut = new ResultSample();
        var userId = 2;
        var productIds = new List<int>
        {
            1,
            2,
            3,
            4
        };
        var createdOrder = (sut.CreateOrder(userId, productIds) as Result<Guid>)!;


        // Act

        var result = sut.GetOrder(createdOrder.Value);

        // Assert
        Assert.True(result.IsSuccess, "Is Success ?");
        Assert.True(result.Code == 200, "Code ?");
        Assert.IsType<Result<ResultSample.Order>>(result);
        Assert.True(result.HasValue, "Has Value ?");
        Assert.True(result.Value!.OrderId == createdOrder.Value, "Order Id ?");
        Assert.True(result.Value.UserId == userId, "User Id ?");
        Assert.True(result.Value.ProductIds.All(val => productIds.Contains(val)), "Product Id ?");
    }
}