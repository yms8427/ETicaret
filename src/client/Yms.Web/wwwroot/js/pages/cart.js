const CartApp = {
    data() {
        return {
            products: [],
            total: 0
        }
    },
    mounted() {
        var self = this;
        this.loadProductsOfCart(self);
    },
    methods: {
        loadProductsOfCart(self) {
            $.ajax({
                url: "/order/getcartitems",
                contentType: "application/json",
                method: "GET",
                success: function (data) {
                    self.products = data.productsOfCart;
                    self.total = data.total;
                    window.localStorage.setItem("cartcount", data.productsOfCart.length);
                }
            });
        },
        getImageUrl(id) {
            return imageUrl.replace("_id_", id);
        },
        increase(product) {
            product.amount++;
            this.updateCart(product);
        },
        decrease(product) {
            if (product.amount > 1) {
                product.amount--;
                this.updateCart(product);
            }
        },
        remove(id, productId) {
            var self = this;
            $.ajax({
                url: `/order/removefromcart/${id}`,
                contentType: "application/json",
                method: "post",
                success: function (data) {
                    if (data) {
                        self.loadProductsOfCart(self);
                        var timer = setInterval(() => {
                            if (connection.state === "Connected") {
                                clearInterval(timer);
                                //TODO: kullanıcı adı
                                connection.invoke("CartDeletion", "can.perk", productId);
                            }
                        }, 100);
                    }
                }
            });
        },
        updateCart(product) {
            product.subTotal = product.amount * product.price;
            this.total = this.products.map(function (p) { return p.subTotal }).reduce(function (prev, next) { return prev + next });
            var p = { productId: product.productId, amount: product.amount };
            $.ajax({
                url: "/order/updatecart",
                contentType: "application/json",
                method: "post",
                data: JSON.stringify(p)
            });
        },
        introduceYourself() {
            connection.invoke("Introduce", "can");
        }
    }
}

Vue.createApp(CartApp).mount('#cart');