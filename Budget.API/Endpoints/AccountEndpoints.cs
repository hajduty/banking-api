using Budget.API.DTOs;
using Budget.Application.Accounts.Commands.CreateAccount;
using Budget.Application.Accounts.Commands.DepositMoney;
using MediatR;

namespace Budget.API.Endpoints;

public static class AccountEndpoints
{
    public static void Map(WebApplication app)
    {
        app.MapPost("/accounts/{accountId}/deposit", async (
            int accountId,
            DepositRequest request,
            ISender sender) =>
        {
            var command = new DepositMoneyCommand(accountId, request.Amount, request.Currency);
            var result = await sender.Send(command);
            return Results.Ok(result);
        });

        app.MapPost("/accounts", async (
            CreateAccountRequest request,
            ISender sender) =>
        {
            var command = new CreateAccountCommand(request.Name, request.Currency);
            var result = await sender.Send(command);
            return Results.Ok(result);
        });
    }
}
