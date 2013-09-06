$('#selFoodA, #selFoodB').select2(
    {
        placeholder: "Search for a food",
        minimumInputLength: 3,
        width: '200px',
        ajax: {
            url: 'api/food',
            dataType: 'json',
            data: function (term) {
                return {
                    search: term
                };
            },
            results: function (data) {

                var dataArray = new Array();
                $.each(data,
                    function () {
                        dataArray.push({ id: this.food_id, text: this.food_name });
                    }
                );
                return { results: dataArray };
            }
        }
    })
    .on("change", function (e) {

        var context = ko.contextFor(this);
        var dataKo = $(e.target).data('ko');

        $.ajax({
            url: 'api/food/' + e.val,
            type: 'GET',
            dataType: 'json',
            success: function (data) {
                data.servings = data.servings.serving;

                ko.mapping.fromJS(data, context.$data['Food' + dataKo]);
            },
            error: function () {
                alert('There was an error with your request');
            }
        });
        return false;
    });

var DetailFood = function () {
    var self = this;
    self.food_id = ko.observable();
    self.food_name = ko.observable();
    self.servings = ko.observableArray();
};

var Nutrients = function () {
    var self = this;
    self.serving_id = ko.observable();
    self.serving_description = ko.observable();
    self.calories = ko.observable();
    self.fat = ko.observable();
    self.saturated_fat = ko.observable();
    self.polyunsaturated_fat = ko.observable();
    self.monounsaturated_fat = ko.observable();
    self.cholesterol = ko.observable();
    self.potassium = ko.observable();
    self.sodium = ko.observable();
    self.carbohydrate = ko.observable();
    self.fiber = ko.observable();
    self.sugar = ko.observable();
    self.protein = ko.observable();
    self.vitamin_a = ko.observable();
    self.vitamin_c = ko.observable();
    self.calcium = ko.observable();
    self.iron = ko.observable();
};

var FoodCompare = function () {

    var self = this;

    self.FoodA = ko.mapping.fromJS(new DetailFood());
    self.FoodB = ko.mapping.fromJS(new DetailFood());

    self.ValueCompare = function (nutrient) {

        var servingA = self.FoodASelectedServing();
        var servingB = self.FoodBSelectedServing();

        var v1, v2;

        if (typeof servingA !== "undefined")
            v1 = servingA[nutrient]();

        if (typeof servingB !== "undefined")
            v2 = servingB[nutrient]();

        if (typeof v1 === "undefined" || typeof v2 === "undefined" || v1 == null || v2 == null)
            return { nutrient: nutrient, FoodAValue: v1, FoodBValue: v2, compareValue: null, compareClass: null };

        var compareDirection = self.Direction(nutrient);
        var compareValue = (v1 - v2);

        var compareClass = (compareValue * compareDirection) < 0 ?
            "danger" : (compareValue * compareDirection) > 0 ?
                "success" : null;

        return { nutrient: nutrient, FoodAValue: v1, FoodBValue: v2, compareValue: compareValue.toFixed(2), compareClass: compareClass };
    };

    self.Direction = function (text) {
        if (text == 'calories')
            return -1;
        else {
            return 1;
        }
    };

    self.FoodASelectedServing = ko.observable();
    self.FoodBSelectedServing = ko.observable();

};

String.prototype.toRegularString = function () {
    return this
        .replace(/_/g, ' ')
        .replace(/^./, function (str) { return str.toUpperCase(); });
};

ko.applyBindings(new FoodCompare());