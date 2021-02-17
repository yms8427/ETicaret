const LayoutApp = {
    data() {
        return {
            mainUrl: "https://localhost:6001/",
            cartAmount: 0,
        }
    },
    methods: {
        GetCartAmount(id) {
            var self = this;
            $.ajax({
                url: `${this.mainUrl}order/getcartamount/${id}`,
                contentType: "application/json",
                method: "GET",
                success: function (data) {
                    self.cartAmount = data;
                }
            });

            return this.cartAmount;
        },
    },
}

Vue.createApp(LayoutApp).mount("#header");