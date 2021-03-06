// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

let editingDrinkId = -1;

$(function () { console.log("Page loaded"); });

function removeDrink(drinkRemoveBtnInfo)
{
	console.log("Page remove event triggered! " + drinkRemoveBtnInfo.id);

	let drinkParsedId = parseInt(drinkRemoveBtnInfo.id.substr(0, drinkRemoveBtnInfo.id.indexOf(' ')));

	$.ajax({
		type: "POST",
		url: "Admin/RemoveDrink",
		data: { drinkId: drinkParsedId },

		success: function (data) {
			let elem = document.getElementById(drinkRemoveBtnInfo.id.substr(0, drinkRemoveBtnInfo.id.indexOf(' ')) + " tr");
			elem.innerHTML = " ";
			elem.outerHTML = "";
			elem.remove();
			console.log("Item removed!");
			editingDrinkId = -1;
		}
	});

	resetInputFields();
}

function startEditingDrink(drinkEditBtnInfo)
{
	let drinkParsedId = parseInt(drinkEditBtnInfo.id.substr(0, drinkEditBtnInfo.id.indexOf(' ')));

	$.ajax({
		type: "POST",
		url: "Admin/GetDrinkEditParams",
		data:
		{
			drinkId: drinkParsedId
		},

		success: function (data)
		{
			console.log("On data: " + data);
			editingDrinkId = drinkParsedId;
			let recievedObject = JSON.parse(data);
			drinkNameTb.value = recievedObject.drinkName;
			drinkAmountTb.value = recievedObject.drinkAmount;
			drinkCostNum.value = recievedObject.drinkCost;
			drinkAvailabilityCb.checked = recievedObject.isDrinkAvailable;
			drinkImgRef.value = recievedObject.img;
			let changeBtn = document.getElementById("changeBtn");
			changeBtn.innerHTML = "Сохранить";
			console.log("Item edit started!");
		}
	});
}

function saveDrinkChanges()
{
	if (editingDrinkId == -1)
	{
		addDrink();
	}
	else
	{
		editDrink();
	}
}

function addDrink()
{
	let drinkNameTb = document.getElementById("drinkNameTb");
	let drinkAmountTb = document.getElementById("drinkAmountTb");
	let drinkCostNum = document.getElementById("drinkCostNum");
	let drinkAvailabilityCb = document.getElementById("drinkAvailabilityCb");
	let drinkImgRef = document.getElementById("drinkImgRef");

	if (drinkAvailabilityCb.checked && parseInt(drinkAmountTb.value) <= 0)
	{
		alert('Для того, чтобы напиток стал доступен, нужно чтобы его количество было больше нуля!');
		return;
	}

	let addRequest =
	{
		drinkId: -1,
		drinkName: drinkNameTb.value,
		drinkAmount: parseInt(drinkAmountTb.value),
		drinkCost: parseInt(drinkCostNum.value),
		isDrinkAvailable: drinkAvailabilityCb.checked,
		img: drinkImgRef.value
	}

	let requestString = JSON.stringify(addRequest);

	$.ajax({
		type: "POST",
		url: "Admin/AddDrink",
		data:
		{
			drinkRequest: requestString
		},

		success: function (data)
		{
			console.log("on data: " + data)
			let table = document.getElementById("tableBody");
			table.innerHTML += data;
			console.log("Item added!");
		}
	});

	resetInputFields();
}

function editDrink(drinkEditBtnInfo)
{
	let drinkNameTb = document.getElementById("drinkNameTb");
	let drinkAmountTb = document.getElementById("drinkAmountTb");
	let drinkCostNum = document.getElementById("drinkCostNum");
	let drinkAvailabilityCb = document.getElementById("drinkAvailabilityCb");
	let drinkImgRef = document.getElementById("drinkImgRef");

	if (drinkAvailabilityCb.checked && parseInt(drinkAmountTb.value) <= 0)
	{
		alert('Для того, чтобы напиток стал доступен, нужно чтобы его количество было больше нуля!');
		return;
	}

	let editRequest =
	{
		drinkId: editingDrinkId,
		drinkName: drinkNameTb.value,
		drinkAmount: parseInt(drinkAmountTb.value),
		drinkCost: parseInt(drinkCostNum.value),
		isDrinkAvailable: drinkAvailabilityCb.checked,
		img: drinkImgRef.value
	}

	let requestString = JSON.stringify(editRequest);

	$.ajax({
		type: "POST",
		url: "Admin/EditDrink",
		data:
		{
			drinkRequest: requestString
		},

		success: function (data)
		{
			console.log("on data: " + data);
			console.log("Item edited!");
			changeBtn.innerHTML = "Создать";
			updateEditedTableElement(editingDrinkId, drinkNameTb.value, drinkAmountTb.value, drinkCostNum.value, drinkAvailabilityCb.checked, drinkImgRef.value);
			resetInputFields();
			editingDrinkId = -1;
		}
	});
}

function resetInputFields()
{
	let drinkNameTb = document.getElementById("drinkNameTb");
	let drinkAmountTb = document.getElementById("drinkAmountTb");
	let drinkCostNum = document.getElementById("drinkCostNum");
	let drinkAvailabilityCb = document.getElementById("drinkAvailabilityCb");
	let drinkImgRef = document.getElementById("drinkImgRef");

	drinkNameTb.value = "";
	drinkAmountTb.value = "";
	drinkCostNum.value = "";
	drinkImgRef.value = "";
	drinkAvailabilityCb.checked = false;
}

function updateEditedTableElement(drinkId, name, amount, cost, availability, img)
{
	let tableTr = document.getElementById(drinkId + " tr");

	console.log(tableTr.children);

	tableTr.children[1].innerHTML = name;
	tableTr.children[2].innerHTML = amount;
	tableTr.children[3].innerHTML = cost;
	tableTr.children[4].innerHTML = availability ? "Да" : "Нет";
	tableTr.children[5].innerHTML = img;
}

function toggleCoin(btn)
{
	let coinValue = parseInt(btn.id.replace("CoinerBtn", ""));
	let isCoinAvailable = false;

	if (btn.innerHTML.includes("Разрешить монеты номиналом"))
		isCoinAvailable = false;
	else
		isCoinAvailable = true;

	isCoinAvailable = !isCoinAvailable;

	$.ajax({
		type: "POST",
		url: "Admin/ToggleCoin",
		data:
		{
			coinValue: coinValue,
			isCoinAvailable: isCoinAvailable
		},

		success: function (data)
		{
			console.log("on data: " + data)
			console.log("Coin " + coinValue + " toggled to " + isCoinAvailable);
		}
	});

	changeCoinerBtnByCoinAvailability(btn, isCoinAvailable);
}

function changeCoinerBtnByCoinAvailability(btn, isCoinAvailable)
{
	if (isCoinAvailable)
	{
		btn.innerHTML = btn.innerHTML.replace("Разрешить монеты номиналом", "Запретить монеты номиналом");
		btn.classList.remove("btn-success");
		btn.classList.add("btn-danger");
	}
	else
	{
		btn.innerHTML = btn.innerHTML.replace("Запретить монеты номиналом", "Разрешить монеты номиналом");
		btn.classList.remove("btn-danger");
		btn.classList.add("btn-success");
	}
}