function showNotification(data) {
    if (data.length == 0) {
        return;
    }
    var icon = 'danger'; //error
    if (data.is_success === true) {
        icon = 'success'; //success
    }
    var msg = 'Cập nhật thành công';
    if (data.is_success === false) {
        msg = data.message;
    }
    
    $.notify({
            // options
            message: '<table cellpadding="3" cellspacing="3"><tr><td nowrap="nowrap">Cập nhật lúc :</td><td nowrap="nowrap"><i>' + Util.date.formatDateTime(new Date()) + '</i></td></tr>' + '<tr><td nowrap="nowrap">Nội dung :</td><td nowrap="nowrap">' + msg + '</td></tr></table>'
        }, {
            // settings
            type: icon,
            placement: {
                    from: 'bottom',
                    align: 'right',
                    animate: {
                            enter: 'animated fadeInDown',
                            exit: 'animated fadeOutUp'
                        }
                }
        });
}
function isNull(data) {
    if (data) {
        return data;
    }
    return '';
}
function paddingDisplay2Number(value) {
    if (value < 10) {
        return '0' + value;
    }
    return value;
}
var Util = {
    distance: {
        meter: {
            formatNoUnit: function (e) {
                if (e) {
                    if (e > 0) {
                        return Util.number.format(parseInt(e / 10) / 10);
                    }
                }
                return 0;
            },
            getDistance: function (e) {
                if (e) {
                    if (e > 0) {
                        return parseInt(e / 10) / 10;
                    }
                }
                return 0;
            }
        },
        gps: {
            formatNoUnit: function (e) {
                if (e) {
                    if (e > 0) {
                        return Util.number.format(parseInt(e / 100) / 10);
                    }
                }
                return 0;
            },
            getDistance: function (e) {
                if (e) {
                    if (e > 0) {
                        return parseInt(e / 100) / 10;
                    }
                }
                return 0;
            }
        }
    },
    number: {
        format: function (e) {
            if (e > 0) {
                parseFloat(e).toFixed(1);
                e += "";
                var t = e.split(".");
                var n = t[0];
                var r = t.length > 1 ? "." + t[1] : "";
                var i = /(\d+)(\d{3})/;
                while (i.test(n)) {
                    n = n.replace(i, "$1" + "," + "$2");
                }
                return n + r;
            }
            return e;
        },
        format2: function (e) {
            if (e == 0) {
                return '';
            }
            if (e > 0) {
                e.toFixed(1);
                e += "";
                var t = e.split(".");
                var n = t[0];
                var r = t.length > 1 ? "." + t[1] : "";
                var i = /(\d+)(\d{3})/;
                while (i.test(n)) {
                    n = n.replace(i, "$1" + "," + "$2");
                }
                return n + r;
            }
            return e;
        },
        format3: function (e) {
            if (e == 0) {
                return '';
            }
            if (e > 0) {
                e.toFixed(1);
                e += "";
                var t = e.split(".");
                var n = t[0];
                var r = t.length > 1 ? "." + t[1] : "";
                var i = /(\d+)(\d{3})/;
                while (i.test(n)) {
                    n = n.replace(i, "$1" + "," + "$2");
                }
                return n + r;
            }
            if (e < 0) {
                var o = 0 - e;
                o.toFixed(1);
                o += "";
                var t1 = o.split(".");
                var n1 = t1[0];
                var r1 = t1.length > 1 ? "." + t1[1] : "";
                var i1 = /(\d+)(\d{3})/;
                while (i1.test(n1)) {
                    n1 = n1.replace(i1, "$1" + "," + "$2");
                }
                return '-' + n1 + r1;
            }
            return e;
        }
    },
    string: {
        format: function (s) {
            if (s) {
                return s;
            }
            return '';
        },
        formatBoolean: function (s) {
            if (s == true) return 'Có';
            else return '';
        }
    },
    date: {
        baseDateTime: function () { return new Date(2010, 0, 1, 0, 0, 0, 0); },
        getSeconds: function (value) {
            if (value) return parseInt((value.getTime() - this.baseDateTime().getTime()) / 1000);
            return 0;
        },
        getDuration: function (date1, date2) { return parseInt((date1.getTime() - date2.getTime()) / 1000); },
        getDuration2: function (date1, date2) { return this.formatTimeFromSeconds(Math.abs(this.getDuration(date1, date2))); },
        getTime: function (seconds) {
            if (seconds == 0) {
                return '';
            }
            var date = this.baseDateTime();
            date.setSeconds(seconds);
            return date;
        },
        getTime2: function (minutes) {
            if (minutes == 0) {
                return '';
            }
            var date = this.baseDateTime();
            date.setMinutes(minutes);
            return date;
        },
        formatDateTime: function (value) {
            if (value) {
                return paddingDisplay2Number(value.getMonth() + 1) + '-' + paddingDisplay2Number(value.getDate()) + ' ' + paddingDisplay2Number(value.getHours()) + ':' + paddingDisplay2Number(value.getMinutes()) + ':' + paddingDisplay2Number(value.getSeconds());
            }
            return '';
        },
        formatDateTime2: function (value) {
            if (value) {
                if (value > 0) {
                    return this.formatDateTime(this.getTime(value));
                }
            }
            return '';
        },
        formatDateTime3: function (value) {
            if (value) {
                return paddingDisplay2Number((value.getYear() + 1900) + '-' + paddingDisplay2Number(value.getDate()) + '-' + paddingDisplay2Number(value.getMonth() + 1) + ' ' + paddingDisplay2Number(value.getHours()) + ':' + paddingDisplay2Number(value.getMinutes()));
            }
            return '';
        },
        formatDateTime4: function (value) {
            if (value) {
                if (value > 0) {
                    return this.formatDateTime3(this.getTime(value));
                }
            }
            return '';
        },
        formatDateTime5: function (value) {
            if (value) {
                return paddingDisplay2Number(value.getMonth() + 1) + '-' + paddingDisplay2Number(value.getDate()) + ' ' + paddingDisplay2Number(value.getHours()) + ':' + paddingDisplay2Number(value.getMinutes());
            }
            return '';
        },
        formatMeterTime: function (value) {
            if (value) {
                if (value > 0) {
                    return this.formatDateTime5(this.getTime(value));
                }
            }
            return '';
        },
        formatDate: function (value) {
            if (value) {
                return paddingDisplay2Number(value.getYear() + 1900) + '-' + paddingDisplay2Number(value.getMonth() + 1) + '-' + paddingDisplay2Number(value.getDate());
            }
            return '';
        },
        formatDate2: function (value) {
            if (value) {
                var date = new Date(value);
                return paddingDisplay2Number(date.getYear() + 1900) + '-' + paddingDisplay2Number(date.getMonth() + 1) + '-' + paddingDisplay2Number(date.getDate());
            }
            return '';
        },
        formatDate3: function (value) {
            if (value) {
                var date = Util.date.getTime(value);
                return paddingDisplay2Number(date.getYear() + 1900) + '-' + paddingDisplay2Number(date.getMonth() + 1) + '-' + paddingDisplay2Number(date.getDate());
            }
            return '';
        },
        formatTime: function (value) {
            if (value) {
                return paddingDisplay2Number(value.getHours()) + ':' + paddingDisplay2Number(value.getMinutes()) + ':' + paddingDisplay2Number(value.getSeconds());
            }
            return '';
        },
        formatTime2: function (value) {
            if (value) {
                return paddingDisplay2Number(value.getHours()) + ':' + paddingDisplay2Number(value.getMinutes());
            }
            return '';
        },
        formatTime3: function (seconds) {
            return this.formatTime2(this.getTime(seconds));
        },
        formatTime4: function (minutes) {
            return this.formatTime2(this.getTime2(minutes));
        },
        formatTimeFromSeconds: function (value) {
            if (value > 0) {
                var results = '';
                var days = parseInt(value / 86400);
                var pad = value % 86400;
                if (days > 0) {
                    results = days + 'd ';
                }
                results = results + paddingDisplay2Number(parseInt(pad / 3600)) + ':' + paddingDisplay2Number(parseInt((pad % 3600) / 60)) + ':' + paddingDisplay2Number(parseInt((pad % 3600) % 60));
                return results;
            }
            return '';
        },
        formatTimeFromSeconds2: function (value) {
            if (value > 0) {
                var results = '';
                var days = parseInt(value / 86400);
                var pad = value % 86400;
                if (days > 0) {
                    results = days + 'd ';
                }
                results = results + paddingDisplay2Number(parseInt(pad / 3600)) + ':' + paddingDisplay2Number(parseInt((pad % 3600) / 60));
                return results;
            }
            return '';
        }
    }
};
function displayAmount(data, type, row, meta) {
    return '<div style="text-align:right;padding-right:10px;">' + Util.number.format(data) + '</div>';
}