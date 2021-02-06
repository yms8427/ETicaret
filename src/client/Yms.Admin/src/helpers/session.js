export default {
    needsAuthentication: function () {
        let s = window.localStorage.getItem("s");
        if (!s) {
            return true;
        }
        return false;
    },
    isAuthenticated: function () {
      return !this.needsAuthentication()  
    },
    getSession: function () {
        return JSON.parse(window.localStorage.getItem("s"));
    },
    authenticate: function (data) {
        window.localStorage.setItem("s", JSON.stringify(data));
    }
}