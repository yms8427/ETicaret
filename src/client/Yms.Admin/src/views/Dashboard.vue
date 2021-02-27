<template>
  <div>
    <CRow>
      <CCol >
        <div class="m-5"><canvas id="planet-chart"></canvas></div>
      </CCol>
    </CRow>
    <CRow>
      <CCol>
        <table class="table table-sm">
          <thead>
            <tr>
              <td>Tarih</td>
              <td>Kullanıcı</td>
              <td>Ürün</td>
            </tr>
          </thead>
          <tbody>
            <tr
              v-for="c in cart"
              :key="c.key"
              :class="{
                removed: c.type === 'REMOVED',
                added: c.type === 'ADDED',
              }"
            >
              <td>{{ c.key }}</td>
              <td>{{ c.name }}</td>
              <td>{{ c.id }}</td>
            </tr>
          </tbody>
        </table>
      </CCol>
    </CRow>
  </div>
</template>

<script>
import hub from "../helpers/hub";
import Chart from "chart.js";
import moment from "moment";
export default {
  name: "Dashboard",
  data: () => ({
    cart: [],
    cartChart: null,
    cartOptions: {
      type: "bar",
      data: {
        labels: [],
        datasets: [
          {
            label: "Sepetten Çıkarılanlar",
            data: [],
            backgroundColor: [
              "rgba(54,73,93,.5)", // Blue
            ],
            borderColor: ["#36495d"],
            borderWidth: 3,
          },
          {
            label: "Sepete Eklenenler",
            data: [],
            backgroundColor: [
              "rgba(71, 183,132,.5)", // Green
            ],
            borderColor: ["#47b784"],
            borderWidth: 3,
          },
        ],
      },
      options: {
        responsive: true,
        lineTension: 1,
        scales: {
          yAxes: [
            {
              ticks: {
                beginAtZero: true,
                padding: 25,
              },
            },
          ],
        },
      },
    },
  }),
  mounted() {
    let timer = setInterval(() => {
      if (hub.state === "Connected") {
        clearInterval(timer);
        hub.invoke("CreateGroup", "admin");
      }
    }, 100);

    this.createChart("planet-chart", this.cartOptions);

    hub.on("ProductRemovedFromCart", (user, id) => {
      let p = createProduct(user, id, "REMOVED");
      this.updateChart(p, 0);
      this.cart.push(p);
    });
    hub.on("ProductAddedToCart", (user, id) => {
      let p = createProduct(user, id, "ADDED");
      this.updateChart(p, 1);
      this.cart.push(p);
    });
  },
  methods: {
    createChart(chartId, chartData) {
      const ctx = document.getElementById(chartId);
      this.cartChart = new Chart(ctx, {
        type: chartData.type,
        data: chartData.data,
        options: chartData.options,
      });
    },
    updateChart(p, index) {
      let time = moment(new Date(p.key), "HH:mm").format("HH:mm");
      if (!this.cartOptions.data.labels.includes(time)) {
        this.cartOptions.data.labels.push(time);
        this.cartOptions.data.datasets[0].data.push(0);
        this.cartOptions.data.datasets[1].data.push(0); 
      }
      this.cartOptions.data.datasets[index].data[this.cartOptions.data.datasets[index].data.length -1]++;
      this.cartChart.update();
    },
  },
};

function createProduct(name, productId, type) {
  return {
    key: new Date().getTime(),
    name: name,
    id: productId,
    type: type,
  };
}
</script>

<style scoped>
.removed {
  background-color: rgb(207, 158, 158);
}
.added {
  background-color: rgb(200, 245, 179);
}
</style>
