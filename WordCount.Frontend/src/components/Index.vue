<template>
  <div class="container">
    <h1>Super Word Count</h1>

    <span class="label">Input</span>
    <textarea class="input" v-model="text"></textarea>

    <div class="button-container">
      <button v-on:click="onSendClick">Send</button>
    </div>

    <table class="results-table">
        <thead>
            <th>Word</th>
            <th>Count</th>
        </thead>
        <tbody>
            <tr v-for="item in wordCounts" :key="item.word">
                <td>{{ item.word }}</td>
                <td>{{ item.rate }}</td>
            </tr>
        </tbody>
    </table>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import WordCountService from "../services/WordCountService";
import WordCountItem from "../models/WordCountItem";

@Component
export default class Idex extends Vue {
  private text: string = "";
  private wordCounts: WordCountItem[] = [];

  onSendClick(): void {
    WordCountService.post(this.text)
      .then(async (response) => {
        if (!response.ok) {
          const resp = await response.json();
          Vue.$toast.open({
            message: resp.errors.Text[0],
            type: "error",
          });
          throw new Error(resp.errors.Text);
        }

        return response.json();
      })
      .then((data: WordCountItem[]) => (this.wordCounts = data))
      .catch((err) => console.error(err));
  }
}
</script>

<style scoped>
.container {
  padding: 15px;
  display: flex;
  flex-direction: column;
  justify-content: space-around;
}
.button-container {
  padding: 15px;
}
.label {
    align-self: flex-start;
    margin-bottom: 15px;
}
.results-table th {
    background: rgb(218, 218, 218);
}
.results-table tr:nth-child(even) {
    background: rgb(218, 218, 218);
}
.input {
  height: 100px;
}
</style>