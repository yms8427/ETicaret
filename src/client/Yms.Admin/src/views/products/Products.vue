<template>
    <div>
        Ürün Listesi
    </div>
</template>
<script>
import ajax from "../../helpers/ajax";
export default {
    name: "Products",
    mounted() {
        ajax.get("api/production/category/list", data => {
            console.log(data);
        });
    }
}
</script>