const LayoutApp = {
    data() {
        return {
            mainUrl: "https://localhost:6001/",
            cartAmount: 0,
        }
    },
    methods: {
        GetCartAmount(id) {
            if (window.localStorage.getItem("cartcount") == null) {
                var self = this;
                $.ajax({
                    url: `${this.mainUrl}order/getcartamount/${id}`,
                    contentType: "application/json",
                    method: "GET",
                    success: function (data) {
                        window.localStorage.setItem("cartcount", data);
                        self.cartAmount = window.localStorage.getItem("cartcount");
                        
                    }
                });

            }else {
                    this.cartAmount = window.localStorage.getItem("cartcount");
            }
            return this.cartAmount;

            
            
        },
    },
}

Vue.createApp(LayoutApp).mount("#header");