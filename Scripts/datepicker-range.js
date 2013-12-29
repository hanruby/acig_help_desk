function pageLoad(sender, args) {

    window.datePicker = function testFun() {
        function convertToDate(input) {
            if (input == undefined) {
                return true;
            }
            var dateArray = input.split('-');
            var output = new Date(dateArray[2], parseInt(dateArray[1]) - 1, dateArray[0]);
            return output;
        }

        var startDateVal = $(".start-date").val();
        if (startDateVal != "") {
            updateDatePickerOfToDate(convertToDate(startDateVal));
        }

        function updateDatePickerOfToDate(minSelectedDate) {
            minSelectedDate.setDate(minSelectedDate.getDate() + 1);
            var maxDate = new Date(minSelectedDate);
            maxDate.setDate(maxDate.getDate() + 14);
            $(".end-date").datepicker('destroy');
            $(".end-date").datepicker({
                constrainInput: true,
                dateFormat: "dd-mm-yy",
                changeMonth: true,
                changeYear: true,
                minDate: minSelectedDate,
                onSelect: function (dateText, inst) {
                }
            });
        }

        $(".start-date").datepicker({
            constrainInput: true,
            dateFormat: "dd-mm-yy",
            changeMonth: true,
            changeYear: true,
            onSelect: function (dateText, inst) {
                var minSelectedDate = convertToDate(dateText);
                $(".end-date").val("");
                updateDatePickerOfToDate(minSelectedDate);
            }
        });

        var minStartDate = 1;
        if ($(".start-date").val() != undefined) {
            minStartDate = convertToDate($(".start-date").val());
            minStartDate.setDate(minStartDate.getDate() + 1);
        }

        $(".end-date").datepicker({
            constrainInput: true,
            dateFormat: "dd-mm-yy",
            changeMonth: true,
            changeYear: true,
            onSelect: function (dateText, inst) {
            }
        });

        $(".start-date, .end-date").attr("readonly", true);
    }
    window.datePicker();
};