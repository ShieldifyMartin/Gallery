export const getMonthString = (monthNum) => {
    var month = [
        "January", "February", "March", "April", "May", "June",
        "July", "August", "September", "October", "November", "December"
    ];
    return month[monthNum - 1];
}