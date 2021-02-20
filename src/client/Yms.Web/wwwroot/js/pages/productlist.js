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
            mainUrl: "https://localhost:6001/",
            count: 1,
        }
    },
    methods: {
        showDetail(id) {
            var self = this;
            $.ajax({
                url: `${this.mainUrl}product/getproductdetail/${id}`,
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
        getAddToCartUrl() {
            var CurrentaddToCartUrl = addToCartUrl.replace("1", this.count);
            return CurrentaddToCartUrl.replace("_id_", this.productDetail.id);
        },
        increase() {
            this.count++;
        },
        decrease() {
            if (this.count > 1) {
                this.count--;
            }
        },
    },
    
    
}

Vue.createApp(ProductListApp).mount("#productlist");