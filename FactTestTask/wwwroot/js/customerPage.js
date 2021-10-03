$(function () { console.log("Page loaded"); });

function addToBalance(toAdd)
{
	$.ajax({
		type: "POST",
		url: "Customer/AddToBalance",
		data: { toAdd: toAdd },

		success: function (data)
		{
			let elem = document.getElementById("balanceH");
			elem.innerHTML = "Баланс: " + data;
			console.log("Added to balance! Now it's " + data);
		}
	});
}

function buyDrink(drinkId)
{
	$.ajax({
		type: "POST",
		url: "Customer/BuyDrink",
		data: { drinkId: drinkId },

		success: function (data)
		{
			let transactionResult = JSON.parse(data);
			let balanceH = document.getElementById("balanceH");

			console.log(data);

			console.log("OC: " + transactionResult.OperationResult);

			let buyBtn = document.getElementById(drinkId + " buyBtn");

			switch (transactionResult.OperationCode)
			{
				case 1:
					balanceH.innerHTML = "Баланс: " + transactionResult.NewBalance;
					break;
				case 2:
					balanceH.innerHTML = "Недостаточно средств (Баланс: " + transactionResult.NewBalance + ")";
					break;
				case 3:
					balanceH.innerHTML = "Баланс: " + transactionResult.NewBalance;
					buyBtn.innerHTML = "Нет в наличии";
					buyBtn.classList.remove("btn-success");
					buyBtn.classList.add("btn-danger");
					buyBtn.disabled = true;
					break;
				case 4:
					balanceH.innerHTML = "Похоже, напиток закончился (Баланс: " + transactionResult.NewBalance + ")";
					buyBtn.innerHTML = "Нет в наличии";
					buyBtn.classList.remove("btn-success");
					buyBtn.classList.add("btn-danger");
					buyBtn.disabled = true;
					break;
				default:
					break;
			}
		}
	});
}