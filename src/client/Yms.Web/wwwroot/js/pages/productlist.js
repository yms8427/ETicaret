const ProductListApp = {
    data() {
        return {
            productDetail: {
                name: "",
                price: null,
                stock: null,
                companyName: "",
                category: "",
                id: "",
                imageId: "",

            },
            productId: "",
        }
    },
    methods: {
        showDetail(id) {
            var self = this;
            $.ajax({
                url: `https://localhost:6001/product/getproductdetail/${id}`,
                contentType: "application/json",
                method: "GET",
                success: function (data) {
                    self.productDetail = data;
                }
            });
        },
        getImageUrl() {
            return imageUrl.replace("_id_", this.productDetail.imageId);
        },
    }
}

Vue.createApp(ProductListApp).mount("#productlist");