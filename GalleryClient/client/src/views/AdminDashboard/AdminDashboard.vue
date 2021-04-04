<template>
  <div class="dashboard">
    <ul class="routes">
      <li><router-link :to="{ name: 'Home', params: { isAdminRoute: true }}">All Posts</router-link></li>
      <li><router-link to="/users">All Users</router-link></li>
    </ul>
    <div class="main">
      <div class="couters">
        <div>
          <h2>{{state.postsCount}}</h2>
          <span>posts count</span>
        </div>
        <div>
          <h2>{{state.usersCount}}</h2>
          <span>users count</span>
        </div>
      </div>
      <div class="stats">
        <img
          src="https://www.statisticshowto.com/wp-content/uploads/2015/08/difference.png"
          alt="statistic"
        />
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, reactive, watchEffect } from "vue";
import { dashboardService } from "../../services";

export default defineComponent({
  setup() {
    const state = reactive({
      postsCount: 0,
      usersCount: 0
    });

    watchEffect(async () => {
      const data = await dashboardService.getDashboardData();
      state.postsCount = data.postsCount;
      state.usersCount = data.usersCount;     
    });

    return {
      state,
    };
  },
});
</script>

<styles src="./index.scss"></styles>
