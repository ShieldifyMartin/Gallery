<template>
  <div class="dashboard">
    <ul class="routes">
      <li><router-link to="/">All Posts</router-link></li>
      <li><router-link to="/users/all">All Users</router-link></li>
    </ul>
    <div class="main">
      <div class="couters">
        <div>
          <h2>3123</h2>
          <span>posts count</span>
        </div>
        <div>
          <h2>11</h2>
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

export default defineComponent({
  setup() {
    const state = reactive({});

    watchEffect(async () => {});

    return {
      state,
    };
  },
});
</script>

<styles src="./index.scss"></styles>
